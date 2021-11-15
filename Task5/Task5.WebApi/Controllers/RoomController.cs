using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5.BLL.DTO;
using Task5.BLL.Interfaces;
using Task5.WebApi.ViewModels.Book;
using Task5.WebApi.ViewModels.Room;

namespace Task5.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly ICategoryService categoryService;
        private readonly ICategoryDateService categoryDateService;
        private readonly IGuestService guestService;
        private readonly IStayService stayService;

        public RoomController(
            IRoomService roomService,
            ICategoryService categoryService,
            ICategoryDateService categoryDateService,
            IGuestService guestService,
            IStayService stayService)
        {
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.categoryDateService = categoryDateService;
            this.guestService = guestService;
            this.stayService = stayService;
        }

        [HttpGet]
        public List<RoomViewModel> Get()
        {
            var response = new List<RoomViewModel>();

            var rooms = roomService.GetAll().ToList();

            var categories = categoryService.GetAll().ToList();

            var categoryDates = categoryDateService.GetAll().Where(x => !x.EndDate.HasValue || x.EndDate > DateTime.Now).OrderBy(x => x.StartDate).ToList();

            for (int i = 0; i < rooms.Count(); i++)
            {
                response.Add(new RoomViewModel()
                {
                    Id = rooms[i].Id,
                    Number = rooms[i].Number,
                    Category = categories.FirstOrDefault(x => x.Id == rooms[i].CategoryId).Name,
                    Price = categoryDates.FirstOrDefault(x => x.CategoryId == rooms[i].CategoryId).Price
                });
            }
            return response;
        }

        [HttpGet]
        public ActionResult<List<RoomViewModel>> GetFreeRooms(DateTime date)
        {
            if (date.Date <= DateTime.Now.Date)
                return BadRequest();

            var response = new List<RoomViewModel>();

            var rooms = roomService.GetAll();

            var categories = categoryService.GetAll();

            var occupiedRoomsId = stayService.GetAll().Where(x => date.Date >= x.StartDate.Date && date.Date <= x.EndDate.Date).Select(x => x.RoomId);

            var categoryDates = categoryDateService.GetAll().Where(x => x.StartDate.Date <= date.Date && (!x.EndDate.HasValue || x.EndDate.Value.Date > date.Date));

            var freeRooms = rooms.Where(x => !occupiedRoomsId.Any(y => y == x.Id));

            foreach (var room in freeRooms)
                response.Add(new RoomViewModel
                {
                    Id = room.Id,
                    Number = room.Number,
                    Category = categories.FirstOrDefault(x => x.Id == room.CategoryId).Name,
                    Price = categoryDates.FirstOrDefault(x => x.CategoryId == room.CategoryId).Price
                });

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<RoomViewModel>> Book(BookRoomViewModel request)
        {
            if (request == null)
                return BadRequest();

            var room = (await roomService.GetAllAsync()).ToList().FirstOrDefault(x => x.Number == request.Number);

            if (room == null)
                return NotFound();

            var categories = await categoryService.GetAllAsync();

            var price = categoryDateService.GetAll()
                .Where(x => x.CategoryId == room.CategoryId && x.StartDate.Date < request.StartDate.Date && (!x.EndDate.HasValue || x.EndDate.Value.Date < request.StartDate.Date))
                .OrderBy(x => x.StartDate).FirstOrDefault().Price;

            var IsOccupied = stayService.GetAll().Where(x => x.RoomId == room.Id)
                .Any(x => (request.StartDate >= x.StartDate && request.StartDate < x.EndDate) || (request.EndDate >= x.StartDate && request.EndDate < x.EndDate));

            if (IsOccupied)
                return StatusCode(412);

            var guest = guestService.GetAll().FirstOrDefault(x => x.Passport == request.Passport);
            if (guest == null)
            {
                guest = new GuestDTO
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Patronymic = request.Patronymic,
                    Passport = request.Passport,
                    BirthDate = request.BirthDate
                };
                await guestService.CreateAsync(guest);
            }

            await stayService.CreateAsync(new StayDTO
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                GuestId = guest.Id,
                RoomId = room.Id
            });

            return new RoomViewModel
            {
                Id = room.Id,
                Number = room.Number,
                Category = categories.FirstOrDefault(x => x.Id == room.CategoryId).Name,
                Price = price
            };

        }
    }
}

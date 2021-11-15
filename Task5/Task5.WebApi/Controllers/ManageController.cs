using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5.BLL.DTO;
using Task5.BLL.Interfaces;
using Task5.WebApi.ViewModels.Book;

namespace Task5.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Moderator,Admin")]
    public class ManageController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly ICategoryDateService categoryDateService;
        private readonly IStayService stayService;

        public ManageController(IRoomService roomService, ICategoryDateService categoryDateService, IStayService stayService)
        {
            this.roomService = roomService;
            this.categoryDateService = categoryDateService;
            this.stayService = stayService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StayViewModel>> GetBookings()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StayDTO, StayViewModel>()).CreateMapper();
            var stays = mapper.Map<IEnumerable<StayDTO>, List<StayViewModel>>(stayService.GetAll());
            return stays;
        }

        [HttpGet]
        public ActionResult<StayViewModel> GetBookings(Guid id)
        {
            var stayDTO = stayService.Get(id);
            if (stayDTO == null)
                return NotFound();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StayDTO, StayViewModel>()).CreateMapper();
            var stay = mapper.Map<StayDTO, StayViewModel>(stayDTO);
            return stay;
        }

        [HttpPut]
        public async Task<ActionResult> CheckIn(Guid id)
        {
            var stay = await stayService.GetAsync(id);
            if (stay == null)
                return NotFound();

            if (stay.StartDate >= DateTime.Now.Date || stay.EndDate < DateTime.Now.Date)
                return BadRequest();

            stay.CheckedIn = true;

            await stayService.UpdateAsync(stay);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> CheckOut(Guid id)
        {
            var stay = await stayService.GetAsync(id);
            if (stay == null)
                return NotFound();

            if (!stay.CheckedIn || stay.CheckedOut)
                return BadRequest();

            if (stay.EndDate.Date > DateTime.Now.Date)
                stay.EndDate = DateTime.Now.Date;

            stay.CheckedOut = true;

            await stayService.UpdateAsync(stay);

            return Ok();
        }

        [HttpGet]
        public ActionResult<decimal> Profit(DateTime startDate, DateTime endDate)
        {
            if (startDate < endDate || endDate > DateTime.Now.Date)
                return BadRequest();

            var stays = stayService.GetAll().Where(x => endDate.Date > x.StartDate.Date && x.EndDate.Date >= startDate.Date);

            var rooms = roomService.GetAll().Where(x => stays.Any(y => y.RoomId == x.Id));

            var categoryDates = categoryDateService.GetAll()
                .Where(x => rooms.Any(y => y.CategoryId == x.CategoryId) && x.StartDate.Date < endDate.Date && (!x.EndDate.HasValue || x.EndDate.Value.Date >= startDate.Date));

            var result = new decimal();
            foreach (var stay in stays)
            {
                var calcStartDay = stay.StartDate > startDate ? stay.StartDate : startDate;

                var calcEndDay = stay.EndDate > endDate ? endDate : stay.EndDate;

                var days = (calcEndDay.Date - calcStartDay.Date).Days;

                var room = rooms.FirstOrDefault(x => x.Id == stay.RoomId);

                var date = startDate;
                while (true)
                {
                    var catDates = categoryDates.FirstOrDefault(x => x.CategoryId == room.CategoryId && x.StartDate.Date <= date.Date && (!x.EndDate.HasValue || x.EndDate.Value > date));
                    if (catDates.EndDate.HasValue)
                    {
                        var calcDays = (catDates.EndDate.Value.Date - date.Date).Days;

                        if (calcDays < days)
                        {
                            result += calcDays * catDates.Price;
                            days -= calcDays;
                            date = catDates.EndDate.Value;
                            continue;
                        }
                    }
                    result += days * catDates.Price;
                    break;
                }
            }
            return Ok(result);
        }
    }
}
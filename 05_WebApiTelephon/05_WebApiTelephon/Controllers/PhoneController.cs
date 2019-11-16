using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _05_WebApiTelephon.Models;
using _05_WebApiTelephon.Providers;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _05_WebApiTelephon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        PhoneProvider phoneProvider;
        public PhoneController()
        {
            phoneProvider = new PhoneProvider();
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var phones = phoneProvider.GetAll();
            return Ok(JsonConvert.SerializeObject(phones));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Phone> Get(int id)
        {
            var phone = phoneProvider.GetPhoneById(id);
            return Ok(phone);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] PhoneModel phone)
        {
            if(phoneProvider.AddNewPhoneAndMessage(phone))
            {
                return Ok("saved");
            }
            return BadRequest("not saved");
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] Phone phone)
        {
            //Phone editedPhone = context.Phones.FirstOrDefault(x => x.Id == phone.Id);
            //editedPhone.Name = phone.Name ?? editedPhone.Name;
            //editedPhone.PhoneNumber = phone.PhoneNumber ?? editedPhone.PhoneNumber;
            //editedPhone.Surname = phone.Surname ?? editedPhone.Surname;

            //context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Phone phone = context.Phones.FirstOrDefault(x => x.Id == id);
            //context.Remove(phone);
            //context.SaveChanges();
        }
    }
}

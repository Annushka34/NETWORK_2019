using _05_WebApiTelephon.Models;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _05_WebApiTelephon.Providers
{
    public class PhoneProvider
    {
        EfContext context;
        public PhoneProvider()
        {
            context = new EfContext();
        }

        public Phone GetPhoneById(int id)
        {
            Phone phone = context.Phones.FirstOrDefault(x => x.Id == id);
            return phone;
        }

        public IEnumerable<Phone> GetAll()
        {
            var phones = context.Phones.Include(x => x.Messages).ToList();
            return phones;
        }

        public bool AddNewPhoneAndMessage(PhoneModel phone)
        {
            Phone newPhone = new Phone();
            newPhone.Name = phone.Name;
            newPhone.PhoneNumber = phone.PhoneNumber;
            newPhone.Surname = phone.Surname;
            context.Add(newPhone);

            Message msg = new Message();
            msg.PhoneId = newPhone.Id;
            msg.Text = phone.Message;
            msg.Date = DateTime.Now;
            context.Add(msg);


            context.SaveChanges();
            return true;
        }
    }
}

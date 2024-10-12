using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static EScanner.App;
using Firebase.Database.Query;
using static EScanner.Includes.GlobalVariables;

namespace EScanner.Models
{
    public class Event
    {
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventStart { get; set; }
        public string EventEnd { get; set; }
        public string EventDate { get; set; }
        public async Task<bool> AddEvent(string frname, string lsname, string uname, string pword, string pwords)
        {
            var user = new Event()
            {
                EventCode = frname,
                EventName = lsname,
                EventStart = uname,
                EventEnd = pword,
                EventDate = pwords
            };
            await client.Child("Event").PostAsync(user);
            return true;
        }
        public async Task<string> GetVisitorKey(string _id)
        {
            var evaluateID = (await client
               .Child("Event")
               .OnceAsync<Event>()).FirstOrDefault
                (a => a.Object.EventCode == _id);
            if (evaluateID != null)
            {
                visitorkey = evaluateID.Key;
                EventCode = evaluateID.Object.EventCode;

                EventName = evaluateID.Object.EventName;
                EventStart = evaluateID.Object.EventStart;
                EventEnd = evaluateID.Object.EventEnd;
                EventDate = evaluateID.Object.EventDate;

                return evaluateID.Key;
            }
            return null;
        }
        //public async Task<bool> EditUserAd(string frname, string lsname, string uname, string pword, string stat, string flename)
        //{

        //    var user = new User()
        //    {
        //        Firstname = frname,
        //        Lastname = lsname,
        //        Username = uname,
        //        Password = pword,
        //    };
        //    await client.Child($"User/{userkey}").PatchAsync(user);
        //    return true;
        //}
        public async Task<bool> GetEvent(string _uname)
        {
            try
            {
                var evaluateUsername = (await client.Child("Event")
                .OnceAsync<Event>()).FirstOrDefault(a =>
                a.Object.EventStart == _uname);


                if (evaluateUsername == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Event>> GetEvent()
        {
            return (await client
                .Child("Event")
                .OnceAsync<Event>()).Select(item => new Event
                {

                    EventCode = item.Object.EventCode,
                    EventName = item.Object.EventName,
                    EventStart = item.Object.EventStart,
                    EventEnd = item.Object.EventEnd,
                    EventDate = item.Object.EventDate

                }).ToList();
        }

        public async Task<String> GetEvents(string _user)
        {
            var evaluateID = (await client.Child("Event")
            .OnceAsync<Event>()).FirstOrDefault
            (a => a.Object.EventCode == _user);
            if (evaluateID != null)
            {

                _ec = evaluateID.Object.EventCode;
                _en = evaluateID.Object.EventName;
                _es = evaluateID.Object.EventStart;
                _ee = evaluateID.Object.EventEnd;
                _ed = evaluateID.Object.EventDate;
                eventkey = evaluateID.Key;

                return evaluateID.Key;
            }
            return null;
        }

        //public async Task<bool> DeleteUser(string user)
        //{
        //    try
        //    {

        //        var getuserkey = (await client
        //            .Child("User")
        //            .OnceAsync<User>()).FirstOrDefault
        //            (a => a.Object.Username == user);
        //        if (getuserkey != null)
        //        {
        //            await client
        //                .Child("User")
        //            .Child(getuserkey.Key)
        //                .DeleteAsync();
        //            client.Dispose();

        //            return true;
        //        }

        //        client.Dispose();
        //        return false;
        //    }
        //    catch
        //    {
        //        client.Dispose();
        //        return false;
        //    }
        //}
        //public async Task<bool> GetUser(string _uname, string _pword)
        //{
        //    try
        //    {
        //        var evaluateUsername = (await client.Child("User")
        //        .OnceAsync<User>()).FirstOrDefault(b =>
        //        b.Object.Username == _uname && b.Object.Password == _pword);


        //        if (evaluateUsername != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> EditUser(string frname, string lsname, string uname, string pword)
        //{
        //    var user = new User()
        //    {
        //        Firstname = frname,
        //        Lastname = lsname,
        //        Username = uname,
        //        Password = pword,
        //    };
        //    await GetStatus(uname);
        //    await client.Child($"User/{userkey}").PatchAsync(user);

        //    return true;
        //}
        //public async Task<List<User>> GetAllUsers()
        //{

        //    return (await client
        //    .Child("User")
        //    .OnceAsync<User>()).Select(item => new User
        //    {
        //        Firstname = item.Object.Firstname,
        //        Lastname = item.Object.Lastname,
        //        Username = item.Object.Username,
        //    }).ToList();
        //}

        //public async Task<List<User>> FindUser(string fname)
        //{
        //    var queryUsers = await GetAllUsers();
        //    await client
        //        .Child("User")
        //        .OnceAsync<User>();
        //    var searchTerms = fname.Split(' ');
        //    return queryUsers.Where(a => searchTerms.Any(term => a.Firstname.ToLower().Contains(term.ToLower()) || a.Username.ToLower().Contains(term.ToLower()) || a.Lastname.ToLower().Contains(term.ToLower())

        //    )).ToList();

        //}
    }
}

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
    public class Visitor
    {
        //public Guid Id { get; set; }
        public string BarcodeId { get; set; }
        public string Purpose { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string EventCode { get; set; }
        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string ID { get; set; }
        public string EventStart { get; set; }



        public async Task<bool> _AddVisitors(string barcodeId, string fullname, string date, string timein, string timeout)
        {
            var visitorKey = await GetEventKey(id);
            var visitor = new Visitor()
            {
            
                BarcodeId = barcodeId,
                FullName = fullname,
                Date = date,
                TimeIn = timein,
                TimeOut = timeout
            };
            await client.Child($"Event/{eventkey}/Attendance").PostAsync(visitor);
            return true;
        }

        public async Task<bool> _OutVisitors(string barcodeId, string fullname, string date, string timein, string timeout)
        {
            //var visitorKey = await GetEventKey(id);
            var visitor = new Visitor()
            {

                BarcodeId = barcodeId,
                FullName = fullname,
                Date = date,
                TimeIn = timein,
                TimeOut = timeout
            };
            await client.Child($"Event/{eventkey}/Attendance/{visitorkey}/").PatchAsync(visitor);
            return true;
        }

        public async Task<Visitor> GetVisitor(string barcodeId)
        {
            var visitors = await client
               .Child($"Event/{eventkey}/Attendance/{visitorkey}")
               .OnceAsync<Visitor>();

            return visitors
               .Where(item => item.Object.BarcodeId == barcodeId)
               .Select(item => new Visitor
               {

                   BarcodeId = item.Object.BarcodeId,

                   FullName = item.Object.FullName,
                   Date = item.Object.Date,
                   TimeIn = item.Object.TimeIn,
                   TimeOut = item.Object.TimeOut
               })
               .FirstOrDefault();
        }

        public async Task<bool> GetVisID(string id)
        {
            try
            {
                var evaluateID =
                    (await client.Child($"Event/{eventkey}/Attendance").OnceAsync<Visitor>()).FirstOrDefault(a =>
                    a.Object.BarcodeId == id);
                if (evaluateID != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetStudID(string id)
        {
            try
            {
                var evaluateID =
                    (await client.Child("Student").OnceAsync<Student>()).FirstOrDefault(a =>
                    a.Object.ID == id);
                if (evaluateID == null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public async Task<bool> GetEventID(string id)
        {
            try
            {
                var evaluateID =
                    (await client.Child($"Event/{eventkey}").OnceAsync<Visitor>()).FirstOrDefault(a =>
                    a.Object.TimeIn == EventStart);
                if (evaluateID != null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetStudKey(string id)
        {
            var evaluateID = (await client
               .Child("Student")
               .OnceAsync<Student>()).FirstOrDefault
                (a => a.Object.ID == id);
            if (evaluateID != null)
            {
                studkey = evaluateID.Key;
                ID = evaluateID.Object.ID;
                FullName = evaluateID.Object.FullName;



                return evaluateID.Key;
            }
            return null;
        }
        public async Task<string> GetEventKey(string id)
        {
            var evaluateID = (await client
               .Child("Event")
               .OnceAsync<Event>()).FirstOrDefault
                (a => a.Object.EventCode == id);
            if (evaluateID != null)
            {
                eventkey = evaluateID.Key;
                EventCode = evaluateID.Object.EventCode;
               EventName = evaluateID.Object.EventName;
              
               

                return evaluateID.Key;
            }
            return null;
        }
        public async Task<string> GetVisitorKey(string barcodeId)
        {
            var evaluateID = (await client
               .Child($"Event/{eventkey}/Attendance")
               .OnceAsync<Visitor>()).FirstOrDefault
                (a => a.Object.BarcodeId == barcodeId);
            if (evaluateID != null)
            {
                visitorkey = evaluateID.Key;
              
               BarcodeId = evaluateID.Object.BarcodeId;
                date = evaluateID.Object.Date;
                fullname = evaluateID.Object.FullName;
                timein = evaluateID.Object.TimeIn;
                timeout = evaluateID.Object.TimeOut;

                return evaluateID.Key;
            }
            return null;
        }

        public async Task<bool> EditVisitor(string barcodeid, string purpose, string fullname, string date, string timein, string timeout)
        {
            try
            {
                var visitorKey = await GetVisitorKey(barcodeid);
                if (visitorKey != null)
                {
                    var visitor = new Visitor()
                    {
                    
                      BarcodeId = barcodeid,
                        FullName = fullname,
                        Date = date,
                        TimeIn = timein,
                        TimeOut = timeout,
                    };

                    await client.Child($"Event/{eventkey}/Attendace/{visitorkey}").PatchAsync(visitor);
                    client.Dispose();
                    return true;
                }
                else
                {
                    return false; // Return false if visitorKey is null
                }
            }
            catch (Exception ex)
            {
                client.Dispose();
                return false;
            }
        }

        //public async Task<bool> DeleteVisitor(string bar)
        //{
        //    try
        //    {
        //        var getvisitorkey = (await client
        //           .Child("Visitor")
        //           .OnceAsync<Visitor>()).FirstOrDefault
        //            (a => a.Object.BarcodeId == id);
        //        if (getvisitorkey != null)
        //        {
        //            await client
        //               .Child("Visitor")
        //           .Child(getvisitorkey.Key)
        //               .DeleteAsync();
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
    }
}

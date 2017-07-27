using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TestSamsaraApi.Models;

namespace TestSamsaraApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            List<Sensor> sensorList = GetSensorList(2401);

            var StartDate = new DateTime(2017, 4, 20, 4, 0, 0);
            var EndDate = new DateTime(2017, 4, 20, 11, 0, 0);

            GetSensorHistory(2401, StartDate, EndDate, 3600000, sensorList, field.pmPhase3Power);

       

         






        }

        public static List<Sensor> GetSensorList(int groupId) {


          //  List<Sensor> sensors = new List<Sensor>();

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.samsara.com");


            var request = new RestRequest();
            // request.AddBody(new { groupId = "2401" });

            var jsonToSend = "{\"groupId\":2401}";

            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.Method = Method.POST;

            request.Resource = "v1/sensors/list";
            request.AddQueryParameter("access_token", "qnxQMV1tOhLXSqZhF98E");


            var response = client.Execute(request);


            JavaScriptSerializer deserial = new JavaScriptSerializer();
            SensorList sList = deserial.Deserialize<SensorList>(response.Content);


            return sList.sensors;
        }

        public static void GetSensorHistory(int groupId, DateTime startDate, DateTime endDate, int step, List<Sensor> sensors, field field)
        {
            long start = ToEpochTime(startDate) * 1000;
            long end = ToEpochTime(endDate) * 1000;

            var seriesList = new List<series>();

            foreach (Sensor s in sensors) {
                series ser = new series();
                ser.widgetId = s.id;
                ser.field = field.ToString();

                seriesList.Add(ser);
            }
       
            
            SensorQuery query = new SensorQuery() { groupId = 2401, startMs = start, endMs = end, stepMs = step, series = seriesList };

            RestClient client = new RestClient()
            {
                BaseUrl = new Uri("https://api.samsara.com")
            };

            var request = new RestRequest();
            // request.AddBody(new { groupId = "2401" });
            JavaScriptSerializer serial = new JavaScriptSerializer();

            var jsonToSend = serial.Serialize(query);

            request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            request.Method = Method.POST;

            request.Resource = "v1/sensors/history";
            request.AddQueryParameter("access_token", "xBE7YA36JSQIgMatJOW4JiFQqxEhO2");


            var response = client.Execute(request);


            int i = 0;


        }

        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        public static long ToEpochTime(DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        public static long ToEpochTime(DateTimeOffset dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> kind.
        /// </summary>
        public static DateTime ToDateTimeFromEpoch(long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(timeInTicks);
        }

        /// <summary>
        /// Converts the given epoch time to a UTC <see cref="DateTimeOffset"/>.
        /// </summary>
        public static DateTimeOffset ToDateTimeOffsetFromEpoch(long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddTicks(timeInTicks);
        }




    }
}

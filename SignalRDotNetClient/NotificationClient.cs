using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRDotNetClient
{
    public class NotificationClient
    {

        HubConnection connection;

        public NotificationClient()
        {

            connection = new HubConnectionBuilder()
                                .WithUrl("http://ebzweb-dt-1d6:96/notificationHub", options =>
                                {
                                    options.Headers.Add("User-GUID", "642609410611072018Comcast.ident");
                                    options.Headers.Add("CIMA-User-Principal-GUID", "642609410611072018Comcast.ident");
                                    options.Headers.Add("User-Login-ID", "thebrt.team@gmail.com");
                                    options.Headers.Add("CIMA-User-Principal-Login-ID", "thebrt.team@gmail.com");
                                    options.Headers.Add("Tracking-ID", "1cbb608a-8067-424b-aa6c-b83b95fb090a");
                                    options.Headers.Add("Authorization", 
                                        "Bearer " +
                                        "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJDdXN0b21lckd1aWQiOiI2NDI2MDk0MTA2MTEwNzIwMThDb21jYXN0LmlkZW50IiwiRmlyc3ROYW1lIjoiQlJUIiwiSXNDb21jYXN0VXNlciI6ZmFsc2UsIkxhc3ROYW1lIjoiVXNlciIsIkxvZ2luRW1haWwiOiJ0aGVicnQudGVhbUBnbWFpbC5jb20iLCJNb2JpbGVQaG9uZU51bWJlciI6bnVsbCwiUGFzc3dvcmRDaGFuZ2VUaW1lU3RhbXAiOiIyMDE4LTA3LTExVDE4OjQxOjA5LjBaIiwiUHJpbWFyeUFjY291bnROdW1iZXIiOiI5MzA5MDQwMjUiLCJTdGF0dXMiOiJBIiwiSGlnaGVzdFJvbGUiOiJBZG1pbmlzdHJhdG9yQmlsbFBheUNvbWJvIiwiUHJpbWFyeUFjY291bnRBdXRoR3VpZCI6Ijg5MTE0NzI1MzQzNTIxMjcwNDIwMThBTSIsIkFjY291bnRMaXN0IjpbeyJBY2NvdW50QmFzaWNJbmZvIjp7IkFjY291bnRIb21lUGhvbmUiOiI3MjAzMjA1Mjc3IiwiQWNjb3VudElkTWFwR3VpZCI6bnVsbCwiQWNjb3VudE51bWJlciI6IjkzMDkwNDAyNSIsIkFjY291bnRQaG9uZSI6bnVsbCwiQWNjb3VudFR5cGUiOiJCIiwiQXV0aEd1aWQiOiI4OTExNDcyNTM0MzUyMTI3MDQyMDE4QU0iLCJDb21wYW55TmFtZSI6IkFCQyBEZW1vIE5ldHdvcmsiLCJGcmllbmRseUFjY291bnROYW1lIjoiQUJDIERlbW8gTmV0d29yayIsIkdyb3VwSWQiOm51bGwsIkxvY2FsQmlsbGVyQWNjb3VudE5vIjpudWxsLCJQcmltYXJ5Q3VzdG9tZXJHdWlkIjoiMjIxNzUyMzQwMzMwMDQyMDE4Q29tY2FzdC5pZGVudCIsIlByb2R1Y3RDYXRlZ29yaWVzIjpbIlNERklSRVdBTEwiLCJTRFdBTiJdLCJUb3RhbElkZW50aXRpZXMiOiIxNyJ9LCJoYXNCdXNpbmVzc0NsYXNzVm9pY2UiOmZhbHNlLCJoYXNFbnRlcnByaXNlVm9pY2UiOmZhbHNlLCJpc1NoZWxsQWNjb3VudCI6dHJ1ZSwiaXNBY3RpdmUiOnRydWUsImlzT25ib2FyZGluZyI6ZmFsc2UsImlzQ29udHJvbGxlckFjY291bnQiOmZhbHNlLCJoYXNBY3RpdmVIc2QiOmZhbHNlLCJpc05vbkNhYmxlIjpmYWxzZSwiaGFzT25ib2FyZGluZ0hzZCI6ZmFsc2UsImhhc0FjdGl2ZVZvaWNlIjpmYWxzZSwiaGFzT25ib2FyZGluZ1ZvaWNlIjpmYWxzZSwiaGFzQWN0aXZlVmlkZW8iOmZhbHNlLCJoYXNPbmJvYXJkaW5nVmlkZW8iOmZhbHNlLCJoYXNMZWdhY3lGaWJlciI6ZmFsc2UsImhhc0V0aGVybmV0IjpmYWxzZSwic2VydmljZUFkZHJlc3MiOnsiYWRkcmVzc1R5cGUiOjAsInN0cmVldEFkZHJlc3NMaW5lMSI6IjE0MyBVbmlvbiBCbHZkIFN0ZSA0MDAiLCJjaXR5IjoiSmVmZmVyc29uIiwic3RhdGUiOiJDTyIsInppcENvZGUiOiI4MDIyOCJ9LCJjb21tZXJjaWFsTG9iTGlzdCI6W10sImN1YnNjcmliZWRTZXJ2aWNlcyI6W10sImlzRW50ZXJwcmlzZSI6dHJ1ZSwiaXNEZHAiOmZhbHNlLCJpc0NzZyI6ZmFsc2UsImlzU21iIjpmYWxzZSwiaXNCdmUiOmZhbHNlLCJtYXJrZXRJZCI6IiIsImNvcnAiOm51bGx9XSwiQWN0aXZlQ29yZVByb2R1Y3RUeXBlIjoiU0RXQU4iLCJDb21wYW55TmFtZSI6IkFCQyBEZW1vIE5ldHdvcmsiLCJJc0RlbW9BZ2VudCI6ZmFsc2UsIlNkV2FuQWNjb3VudE51bWJlciI6IjkzMDkwNDAyNSIsIkltcGVyc29uYXRvcklkIjpudWxsfQ.8Joyq5iwdZQCIaq-tOLmebkOmfvYRTjZnckFlxtbkgg");
                                })
                                .Build();

            connection.Closed += async (error) =>
            {
                // This will make recursive calls to start connections.
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string>(
                "ReceiveMessage", 
                (user, message) => 
                {

                });
        }

        public async Task StartConnection()
        {
            try
            {
                await connection.StartAsync();

            }
            catch(Exception ex)
            {
                var x = ex;
            }
        }

        public async Task SendMessage(string topic)
        {
                await connection.InvokeAsync(
                    "Publish",
                    new PublishNotificationModel
                    {
                        GroupName = "ActiveCore/" + topic,
                        Data = new { user = "Every one", message = topic },
                        Method = topic
                    });
         
        }

        public async Task SendMessage(string topic, string account)
        {
                await connection.InvokeAsync(
                    "Publish",
                    new PublishNotificationModel
                    {
                        GroupName = "ActiveCore/" + topic + "/" + account,
                        Data = new { account, message= topic },
                        Method = topic
                    });
         
        }

        public async Task SendList()
        {
            try
            {
                await connection.InvokeAsync(
                    "SendList",
                    new List<Data> 
                    { 
                        new Data 
                        {
                             Message = "meaoooo",
                             Name = "Tom"
                        },
                        new Data 
                        {
                            Message = "cheese",
                            Name = "jerry"
                        }
                    });
            }
            catch (Exception ex)
            {
                var z = ex;
            }

        }

        public async Task SendArray()
        {
            try
            {
                await connection.InvokeAsync(
                    "SendArray",
                    new Data[] 
                    {
                        new Data
                        {
                            Message = "meaoooo",
                            Name = "Tom"
                        },
                         new Data
                        {
                            Message = "cheese",
                            Name = "jerry"
                        }
                    });
            }
            catch (Exception ex)
            {
                var z = ex;
            }

        }

        public async Task SendObject()
        {
            try
            {
                await connection.InvokeAsync(
                    "SendObject",
                    new Data { Message = "HELLO", Name="BATMAN" });
            }
            catch (Exception ex)
            {
                var z = ex;
            }

        }

        public async Task SendOptonal()
        {
            try
            {
                await connection.InvokeAsync(
                    "SendOptional",
                    "OOPS");
            }
            catch (Exception ex)
            {
                var z = ex;
            }

        }

        public async Task SendAlert()
        {
            try
            {
                //var user = _connection.User;

                await connection.InvokeAsync(
                    "DeviceDown",
                    new NotificationModel
                    { 
                        DeviceClli = "Device 1",
                        SiteClli = "Site 1",
                        Status = "Online"
                    });
            }
            catch (Exception ex)
            {
                var z = ex;
            }

        }

    }

    public class Data
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Notification model.
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Gets or sets the device clli.
        /// </summary>
        /// <value>The device clli.</value>
        public string DeviceClli { get; set; }

        /// <summary>
        /// Gets or sets the site clli.
        /// </summary>
        /// <value>The site clli.</value>
        public string SiteClli { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status site/device.</value>
        public string Status { get; set; }

        public string Topic { get; set; }
        public string Group { get; set; }

    }

    public class PublishNotificationModel
    {
        public string GroupName { get; set; }
        public string Method { get; set; }
        public object Data { get; set; }
    }
}

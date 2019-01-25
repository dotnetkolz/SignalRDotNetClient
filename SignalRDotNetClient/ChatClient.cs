using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRDotNetClient
{
    public class ChatClient
    {
        HubConnection connection;
        public ChatClient()
        {
            //connection = new HubConnectionBuilder()
            //.WithUrl("wss://localhost:5001/chatHub")
            //.Build();
            //https://localhost:5001/notificationHub

            connection = new HubConnectionBuilder()
                                .WithUrl("http://ebzweb-dt-1d6:96/notificationHub", options => 
                                {
                                    options.Headers.Add("User-GUID", "169040180506092018Comcast.ident");
                                    options.Headers.Add("CIMA-User-Principal-GUID", "169040180506092018Comcast.ident");
                                    options.Headers.Add("User-Login-ID", "lee_berk@yopmail.com");
                                    options.Headers.Add("CIMA-User-Principal-Login-ID", "lee_berk@yopmail.com");
                                    options.Headers.Add("Tracking-ID", "7cbb608a-8067-424b-aa6c-b83b95fb090a");
                                    options.Headers.Add("Authorization", 
                                        "Bearer " +
                                        "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJDdXN0b21lckd1aWQiOiIxNjkwNDAxODA1MDYwOTIwMThDb21jYXN0LmlkZW50IiwiRmlyc3ROYW1lIjoibGVlIiwiSXNDb21jYXN0VXNlciI6ZmFsc2UsIkxhc3ROYW1lIjoiYmVyayIsIkxvZ2luRW1haWwiOiJsZWVfYmVya0B5b3BtYWlsLmNvbSIsIk1vYmlsZVBob25lTnVtYmVyIjpudWxsLCJQYXNzd29yZENoYW5nZVRpbWVTdGFtcCI6IjIwMTgtMDktMDZUMTc6MTg6NDAuMFoiLCJQcmltYXJ5QWNjb3VudE51bWJlciI6IjkzNDUxMzI5NiIsIlN0YXR1cyI6IkEiLCJIaWdoZXN0Um9sZSI6IkFkbWluaXN0cmF0b3IiLCJQcmltYXJ5QWNjb3VudEF1dGhHdWlkIjoiOTQxMTQ0NDMwNjE5MDYyMDE4Q29tY2FzdC5BU1VCIiwiQWNjb3VudExpc3QiOlt7IkFjY291bnRCYXNpY0luZm8iOnsiQWNjb3VudEhvbWVQaG9uZSI6bnVsbCwiQWNjb3VudElkTWFwR3VpZCI6bnVsbCwiQWNjb3VudE51bWJlciI6IjkzNDUxMzI5NiIsIkFjY291bnRQaG9uZSI6bnVsbCwiQWNjb3VudFR5cGUiOiJCIiwiQXV0aEd1aWQiOiI5NDExNDQ0MzA2MTkwNjIwMThDb21jYXN0LkFTVUIiLCJDb21wYW55TmFtZSI6Ik5hdGlvbmFsX0luZGlyZWN0X0RlbW8iLCJGcmllbmRseUFjY291bnROYW1lIjoiTmF0aW9uYWxfSW5kaXJlY3RfRGVtbyIsIkdyb3VwSWQiOm51bGwsIkxvY2FsQmlsbGVyQWNjb3VudE5vIjoiOTM0NTEzMjk2IiwiUHJpbWFyeUN1c3RvbWVyR3VpZCI6Ijg4NDcyNjU3MDYxOTA2MjAxOENvbWNhc3QuaWRlbnQiLCJQcm9kdWN0Q2F0ZWdvcmllcyI6WyJTRFdBTiJdLCJUb3RhbElkZW50aXRpZXMiOiIxMTQifSwiaGFzQnVzaW5lc3NDbGFzc1ZvaWNlIjpmYWxzZSwiaGFzRW50ZXJwcmlzZVZvaWNlIjpmYWxzZSwiaXNTaGVsbEFjY291bnQiOnRydWUsImlzQWN0aXZlIjp0cnVlLCJpc09uYm9hcmRpbmciOmZhbHNlLCJpc0NvbnRyb2xsZXJBY2NvdW50IjpmYWxzZSwiaGFzQWN0aXZlSHNkIjpmYWxzZSwiaXNOb25DYWJsZSI6ZmFsc2UsImhhc09uYm9hcmRpbmdIc2QiOmZhbHNlLCJoYXNBY3RpdmVWb2ljZSI6ZmFsc2UsImhhc09uYm9hcmRpbmdWb2ljZSI6ZmFsc2UsImhhc0FjdGl2ZVZpZGVvIjpmYWxzZSwiaGFzT25ib2FyZGluZ1ZpZGVvIjpmYWxzZSwiaGFzTGVnYWN5RmliZXIiOmZhbHNlLCJoYXNFdGhlcm5ldCI6ZmFsc2UsInNlcnZpY2VBZGRyZXNzIjp7ImFkZHJlc3NUeXBlIjowLCJzdHJlZXRBZGRyZXNzTGluZTEiOiIxODAwIEJJU0hPUFMgR0FURSBCTFZEIiwiY2l0eSI6Ik10LiBMYXVyZWwiLCJzdGF0ZSI6Ik5KIiwiemlwQ29kZSI6IjA4MDU0In0sImNvbW1lcmNpYWxMb2JMaXN0IjpbXSwiY3Vic2NyaWJlZFNlcnZpY2VzIjpbXSwiaXNFbnRlcnByaXNlIjp0cnVlLCJpc0RkcCI6ZmFsc2UsImlzQ3NnIjpmYWxzZSwiaXNTbWIiOmZhbHNlLCJpc0J2ZSI6ZmFsc2UsIm1hcmtldElkIjoiIiwiY29ycCI6bnVsbH1dLCJBY3RpdmVDb3JlUHJvZHVjdFR5cGUiOiJTRFdBTiIsIkNvbXBhbnlOYW1lIjoiTmF0aW9uYWxfSW5kaXJlY3RfRGVtbyIsIklzRGVtb0FnZW50IjpmYWxzZSwiU2RXYW5BY2NvdW50TnVtYmVyIjoiOTM0NTEzMjk2IiwiSW1wZXJzb25hdG9ySWQiOm51bGx9.AmMMQlPu0MZPVkK5HY5aNwCaMdN8bQafALUVzE4HxCg");

                                    //options.Cookies.Add()
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

        //public Task<string> AccessToken()
        //{
        //    return "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJDdXN0b21lckd1aWQiOiIxNjkwNDAxODA1MDYwOTIwMThDb21jYXN0LmlkZW50IiwiRmlyc3ROYW1lIjoibGVlIiwiSXNDb21jYXN0VXNlciI6ZmFsc2UsIkxhc3ROYW1lIjoiYmVyayIsIkxvZ2luRW1haWwiOiJsZWVfYmVya0B5b3BtYWlsLmNvbSIsIk1vYmlsZVBob25lTnVtYmVyIjpudWxsLCJQYXNzd29yZENoYW5nZVRpbWVTdGFtcCI6IjIwMTgtMDktMDZUMTc6MTg6NDAuMFoiLCJQcmltYXJ5QWNjb3VudE51bWJlciI6IjkzNDUxMzI5NiIsIlN0YXR1cyI6IkEiLCJIaWdoZXN0Um9sZSI6IkFkbWluaXN0cmF0b3IiLCJQcmltYXJ5QWNjb3VudEF1dGhHdWlkIjoiOTQxMTQ0NDMwNjE5MDYyMDE4Q29tY2FzdC5BU1VCIiwiQWNjb3VudExpc3QiOlt7IkFjY291bnRCYXNpY0luZm8iOnsiQWNjb3VudEhvbWVQaG9uZSI6bnVsbCwiQWNjb3VudElkTWFwR3VpZCI6bnVsbCwiQWNjb3VudE51bWJlciI6IjkzNDUxMzI5NiIsIkFjY291bnRQaG9uZSI6bnVsbCwiQWNjb3VudFR5cGUiOiJCIiwiQXV0aEd1aWQiOiI5NDExNDQ0MzA2MTkwNjIwMThDb21jYXN0LkFTVUIiLCJDb21wYW55TmFtZSI6Ik5hdGlvbmFsX0luZGlyZWN0X0RlbW8iLCJGcmllbmRseUFjY291bnROYW1lIjoiTmF0aW9uYWxfSW5kaXJlY3RfRGVtbyIsIkdyb3VwSWQiOm51bGwsIkxvY2FsQmlsbGVyQWNjb3VudE5vIjoiOTM0NTEzMjk2IiwiUHJpbWFyeUN1c3RvbWVyR3VpZCI6Ijg4NDcyNjU3MDYxOTA2MjAxOENvbWNhc3QuaWRlbnQiLCJQcm9kdWN0Q2F0ZWdvcmllcyI6WyJTRFdBTiJdLCJUb3RhbElkZW50aXRpZXMiOiIxMTQifSwiaGFzQnVzaW5lc3NDbGFzc1ZvaWNlIjpmYWxzZSwiaGFzRW50ZXJwcmlzZVZvaWNlIjpmYWxzZSwiaXNTaGVsbEFjY291bnQiOnRydWUsImlzQWN0aXZlIjp0cnVlLCJpc09uYm9hcmRpbmciOmZhbHNlLCJpc0NvbnRyb2xsZXJBY2NvdW50IjpmYWxzZSwiaGFzQWN0aXZlSHNkIjpmYWxzZSwiaXNOb25DYWJsZSI6ZmFsc2UsImhhc09uYm9hcmRpbmdIc2QiOmZhbHNlLCJoYXNBY3RpdmVWb2ljZSI6ZmFsc2UsImhhc09uYm9hcmRpbmdWb2ljZSI6ZmFsc2UsImhhc0FjdGl2ZVZpZGVvIjpmYWxzZSwiaGFzT25ib2FyZGluZ1ZpZGVvIjpmYWxzZSwiaGFzTGVnYWN5RmliZXIiOmZhbHNlLCJoYXNFdGhlcm5ldCI6ZmFsc2UsInNlcnZpY2VBZGRyZXNzIjp7ImFkZHJlc3NUeXBlIjowLCJzdHJlZXRBZGRyZXNzTGluZTEiOiIxODAwIEJJU0hPUFMgR0FURSBCTFZEIiwiY2l0eSI6Ik10LiBMYXVyZWwiLCJzdGF0ZSI6Ik5KIiwiemlwQ29kZSI6IjA4MDU0In0sImNvbW1lcmNpYWxMb2JMaXN0IjpbXSwiY3Vic2NyaWJlZFNlcnZpY2VzIjpbXSwiaXNFbnRlcnByaXNlIjp0cnVlLCJpc0RkcCI6ZmFsc2UsImlzQ3NnIjpmYWxzZSwiaXNTbWIiOmZhbHNlLCJpc0J2ZSI6ZmFsc2UsIm1hcmtldElkIjoiIiwiY29ycCI6bnVsbH1dLCJBY3RpdmVDb3JlUHJvZHVjdFR5cGUiOiJTRFdBTiIsIkNvbXBhbnlOYW1lIjoiTmF0aW9uYWxfSW5kaXJlY3RfRGVtbyIsIklzRGVtb0FnZW50IjpmYWxzZSwiU2RXYW5BY2NvdW50TnVtYmVyIjoiOTM0NTEzMjk2IiwiSW1wZXJzb25hdG9ySWQiOm51bGx9.AmMMQlPu0MZPVkK5HY5aNwCaMdN8bQafALUVzE4HxCg";
        //}

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

        public async Task SendMessage()
        {
            try
            {
                //await connection.StartAsync();
                await connection.InvokeAsync(
                    "SendMessage",
                    "API",
                    "Hello");
            }
            catch(Exception ex)
            {
                var z = ex;
            }

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
                await connection.InvokeAsync(
                    "SendAlert",
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
    }
}

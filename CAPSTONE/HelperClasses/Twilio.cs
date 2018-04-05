using CAPSTONE.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CAPSTONE.HelperClasses
{
    public class Twilio
    {
        string accountSid;
        string authToken;

        public Twilio()
        {
            accountSid = APIKeys.TwilioKey;
            authToken = APIKeys.TwilioAuthToken;
            TwilioClient.Init(accountSid, authToken);
        }

        public void Send(Message message, string phoneNumber)
        {
                var smsMessage = MessageResource.Create(
                    to: new PhoneNumber(phoneNumber),
                    from: new PhoneNumber("7632735896"),
                    body: message.content);
            
        }

    }
}
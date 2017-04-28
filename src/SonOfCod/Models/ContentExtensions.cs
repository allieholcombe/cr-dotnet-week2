﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SonOfCod.Models
{
    public static class ContentExtensions
    {
        public static void EnsureSeedData(this SOCContext context)
        {
            if(!context.Content.Any())
            {
                var funBlurb = new Models.Content
                {
                    Title = "World's Best Seafood",
                    Text = "We expect ourselves to do business right, to lead by example and to help out when we are needed. It is our company philosophy that guides our everyday decisions. It is good to be responsible, not just because it is the right thing to do, but because it also sets the bar for our company’s commitment to ensure that the communities in which we work and live will continue to prosper.",
                    ClickText = "Learn more",
                    ClickView = "Index",
                    ClickController = "Home"
                };

                context.Content.Add(funBlurb);

                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Lenguages
    {
        static Lenguages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Title
        {
            get { return Resource.Title; }
        }
    }
    }


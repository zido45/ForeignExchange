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

        public static string AmountValidation
        {
            get { return Resource.AmountValidation; }
        }

        public static string ErrorTitle
        {
            get { return Resource.ErrorTitle; }
        }

        public static string LoadingRates
        {
            get { return Resource.LoadingRates; }
        }

        public static string ReadytoConvert
        {
            get { return Resource.ReadytoConvert; }
        }

        public static string SourceRate
        {
            get { return Resource.SourceRate; }
        }

        public static string TargetRate
        {
            get { return Resource.TargetRate; }
        }

        public static string ValueValidation
        {
            get { return Resource.ValueValidation; }
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string AmountPlaceholder
        {
            get { return Resource.AmountPlaceholder; }
        }

        public static string SourceRatePlaceholder
        {
            get { return Resource.SourceRatePlaceholder; }
        }

        public static string TargetRatePlaceholder
        {
            get { return Resource.TargetRatePlaceholder; }
        }

        public static string SelectSourceRate
        {
            get { return Resource.SelectSourceRate; }
        }

        public static string SelectTargetRate
        {
            get { return Resource.SelectTargetRate; }
        }

        public static string Amount
        {
            get { return Resource.Amount; }
        }
    }

}

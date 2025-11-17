namespace Nihdi.DevoLearning.Presentation.Shared.Models
{
    public class LocalizedStringModel
    {
        public string ValueFr
        {
            get; set;
        }

        public string ValueNl
        {
            get; set;
        }

        public string LocalizedValue
        {
            get
            {
                var culture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                return culture switch
                {
                    "fr" => ValueFr ?? ValueNl ?? "-",
                    "nl" => ValueNl ?? ValueFr ?? "-",
                    _ => ValueFr ?? ValueNl ?? "-"
                };
            }
        }
    }
}

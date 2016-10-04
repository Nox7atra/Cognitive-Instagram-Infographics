using CognitiveInsta.Core;
using CognitiveInsta.Core.WebInfographic;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CognitiveInsta
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //From MS Cognitive Services Emotions API
        private readonly string API_KEY = "YOUR API KEY";
        private readonly string TEMPLATE_NAME = "template.html";
        public readonly static string DATA_FOLDER_NAME = "data";

        private ProfileInfo _CurrentProfile;

        public MainPage()
        {
            _CurrentProfile = new ProfileInfo();
            CreateInfographic();


            this.InitializeComponent();
        }
        public async void CreateInfographic()
        {
            //without @
            await _CurrentProfile.LoadProfileData("INSTAGRAM ACCOUNT NAME", API_KEY);
            WebInfographicCreator creator = new WebInfographicCreator();
            await creator.CreateWebInfoGraphic(_CurrentProfile, TEMPLATE_NAME);
        }

    }
}

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Java.IO;
using Microsoft.Xna.Framework;
using Synergy_HW.Lesson;
using System.Text;

namespace Synergy_HW
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private Game1 _game;
        private View _view;
        static readonly int READ_REQUEST_CODE = 1337;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new Game1();
            _view = _game.Services.GetService(typeof(View)) as View;

            Intent intent = new Intent(Intent.ActionOpenDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("text/plain");

            StartActivityForResult(intent, READ_REQUEST_CODE);

            SetContentView(_view);
            _game.Run();
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == READ_REQUEST_CODE && resultCode == Result.Ok)
            {
                // The document selected by the user won't be returned in the intent.
                // Instead, a URI to that document will be contained in the return intent
                // provided to this method as a parameter.  Pull that uri using "resultData.getData()"
                if (data != null)
                {
                    Android.Net.Uri uri = data.Data;
                    var file = new File(uri.Path);
                    FileInputStream stream = new(file);
                    Schedule.Initialize(Encoding.UTF8.GetString(stream.ReadAllBytes()));
                    // Then you can operate the file with input and output stream
                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}

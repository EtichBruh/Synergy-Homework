using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
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
        static readonly int MANAGEALLFILESACCESSPERMISSION = 2296;
        static readonly int STORAGE_PERMISSION_CODE = 101;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new Game1();
            _view = _game.Services.GetService(typeof(View)) as View;

            if (CheckSelfPermission(Manifest.Permission.ManageExternalStorage) == Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, [Manifest.Permission.ManageExternalStorage], STORAGE_PERMISSION_CODE);

                try
                {
                    Intent intent = new Intent(Settings.ActionManageAllFilesAccessPermission, Android.Net.Uri.Parse($"package:{Application.PackageName}"));
                    //intent.AddCategory("android.intent.category.DEFAULT");
                    StartActivityForResult(intent, MANAGEALLFILESACCESSPERMISSION);
                }
                catch { }
            }

            Intent open = new Intent(Intent.ActionOpenDocument);
            open.SetType("application/json");
            open.AddCategory(Intent.CategoryOpenable);

            StartActivityForResult(Intent.CreateChooser(open, "Select file"), READ_REQUEST_CODE);

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
                    var file = new File(uri.ToString());
                    var fd = ContentResolver.OpenFileDescriptor(uri, "r");
                    FileInputStream stream = new(ContentResolver.OpenFileDescriptor(uri, "r").FileDescriptor);
                    Schedule.Initialize(stream.ReadAllBytes());
                    stream.Close();
                    fd.Close();
                    // Then you can operate the file with input and output stream
                }
            }
            else if (requestCode == MANAGEALLFILESACCESSPERMISSION)
            {
                if (resultCode == Result.Ok)
                    Toast.MakeText(this, "MANAGEAPPFILEACCESSPERMISSION OK", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "MANAGEAPPFILEACCESSPERMISSION NOT OK", ToastLength.Short).Show();
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == STORAGE_PERMISSION_CODE && grantResults.Length > 0)
            {
                if (grantResults[0] == Permission.Granted)
                {
                    Toast.MakeText(this, "Storage Permission Granted", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Storage Permission Denied", ToastLength.Long).Show();
                }
            }
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

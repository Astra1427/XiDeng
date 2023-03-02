using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using XiDeng.IService;
using XiDeng.Droid; 
using Xamarin.Forms;
using Android.Provider;
using Android.Graphics;
using Android;
using AndroidX.Core.Content;
using AndroidX.Core.App;
[assembly : Dependency(typeof(PhotoPickerService))]
[assembly : Dependency(typeof(VideoPickerService))]
namespace XiDeng.Droid
{
    [Activity(Label = "熄灯", Icon = "@drawable/xd_logo_02", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            if ((ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 0);
            }
            LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.Black);
            Instance = this;
        }
        public static readonly int PickImageId = 1000;
        public static readonly int PickVideoId = 1001;
        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { get; set; }
        public TaskCompletionSource<string> PickVideoTaskCompletionSource { get; set; }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == PickImageId)
            {
                if (resultCode == Result.Ok && data != null)
                {
                    Android.Net.Uri uri = data.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri); 
                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
            else if (requestCode == PickVideoId)
            {
                if (resultCode == Result.Ok && data != null)
                {
                    // Set the Stream as the completion of the Task
                    Stream stream = ContentResolver.OpenInputStream(data.Data) ;
                    //PickVideoTaskCompletionSource.SetResult();
                }
                else
                {
                    PickVideoTaskCompletionSource.SetResult(null);
                }
            }
        }
        public string GetPath(Android.Net.Uri uri)
        {
            //string[] projection = MediaStore.Video.Media.DATA;
            Android.Database.ICursor cursor = ContentResolver.Query(uri,null,null,null,null);
            if (cursor != null)
            {
                //int column_index = cursor.GetColumnIndexOrThrow(MediaStore.Video.Media.DATA);
                cursor.MoveToFirst();
                //return cursor.GetString(column_index);
                return null;
            }
            else
                return null;
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
    public class PhotoPickerService : IPhotoPickerService
    {
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent,"Select Picture"),MainActivity.PickImageId);
            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();
            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
    public class VideoPickerService : IVideoPickerService
    {
        public Task<string> GetVideoFileNameAsync()
        {
            Intent intent = new Intent();
            intent.SetType("video/*");
            intent.SetAction(Intent.ActionGetContent);
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent,"选择视频"), MainActivity.PickVideoId) ;
            MainActivity.Instance.PickVideoTaskCompletionSource = new TaskCompletionSource<string>();
            return MainActivity.Instance.PickVideoTaskCompletionSource.Task;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WeldingMask.Droid.Renderers;

namespace WeldingMask.Droid
{
    [Activity(Label = "CameraActivity")]
    public class CameraActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            ActionBar.Hide();
            SetContentView(Resource.Layout.activity_camera);

            if (savedInstanceState == null)
            {
                FragmentManager.BeginTransaction().Replace(Resource.Id.container, Camera2BasicFragment.NewInstance()).Commit();
            }

        }
    }
}

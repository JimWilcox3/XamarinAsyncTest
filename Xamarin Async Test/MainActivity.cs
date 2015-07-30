using System;
using System.ServiceModel;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using WcfService2;

namespace Xamarin_Async_Test
{
    [Activity(Label = "Xamarin_Async_Test", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        TextView MyText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            MyText = FindViewById<TextView>(Resource.Id.MyText);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.Click += CallService1;
        }

        private async void CallService1(object sender, EventArgs e)
        {
            BasicHttpBinding binding = DataModel.CreateBasicHttp();

            //Service1Client client = new Service1Client(binding, new EndpointAddress("http://localhost:59074/Service1.svc"));
            Service1Client client = new Service1Client(binding, new EndpointAddress("http://192.168.1.96:59075/Service1.svc"));

            count++;

            CompositeType parm = new CompositeType();
            parm.BoolValue = true;
            parm.StringValue = "Hello " + count.ToString() + " ";

            try
            {
                CompositeType s = await Task<CompositeType>.Factory.FromAsync((asyncCallback, asyncState) => client.BeginGetDataUsingDataContract(parm, asyncCallback, asyncState),
                                                                       (asyncResult) => client.EndGetDataUsingDataContract(asyncResult), null);

                MyText.Text = s.StringValue;
            }
            catch(Exception ex)
            {
                Android.Util.Log.Debug("", ex.ToString());
            }


        }
    }

}


using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using Android.Locations;
using System.Linq;
using System.Collections.Generic;
using Android.Runtime;
using Android.Gms.Common.Apis;
using Android.Views;
using Android.Graphics;

namespace Map
{
    [Activity(Label = "Map", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback,ILocationListener
    {
        private static int SWIPE_THRESHOLD = 100;
        private static int SWIPE_VELOCITY_THRESHOLD = 100;
        int maptype=GoogleMap.MapTypeSatellite;
        LatLng latlng;
        string locationProvider;
        Location currentLocation;
        LocationManager locationManager;
      
        private GoogleMap GMap;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            InitializeLocationManager();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
           SetSupportActionBar(toolbar);
            toolbar.SetBackgroundColor(Color.ParseColor("#FF8BCEF2"));

            SetUpMap();
           
        }
        public void OnMapReady(GoogleMap googleMap)
        {

            this.GMap = googleMap;

            GMap.MapType = maptype;
            GMap.MyLocationEnabled = true;
            GMap.UiSettings.ZoomControlsEnabled = true;

            if (currentLocation == null)
            {
                if (locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
                    currentLocation = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                else
                    currentLocation = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);

            }
            else
            {
                latlng = new LatLng(currentLocation.Latitude, currentLocation.Longitude);
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
                GMap.MoveCamera(camera);
                MarkerOptions options = new MarkerOptions().SetPosition(latlng);
                GMap.AddMarker(options);
            }


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.menu, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                
                case Resource.Id.Hybrid:
                    if(maptype==GoogleMap.MapTypeHybrid)
                    {
                        return true;
                    }
                    maptype = GoogleMap.MapTypeHybrid;
                   
                    OnMapReady(GMap);
                    return true;
                case Resource.Id.Satellite:
                    if (maptype == GoogleMap.MapTypeSatellite)
                    {
                        return true;
                    }
                    maptype = GoogleMap.MapTypeSatellite;
                    OnMapReady(GMap);
                    return true;
                case Resource.Id.Normal:
                    if (maptype == GoogleMap.MapTypeNormal)
                    {
                        return true;
                    }
                    maptype = GoogleMap.MapTypeNormal;
                    OnMapReady(GMap);
                    return true;
                case Resource.Id.Terrain:
                    if (maptype == GoogleMap.MapTypeTerrain)
                    {
                        return true;
                    }
                    maptype = GoogleMap.MapTypeTerrain;
                    OnMapReady(GMap);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        private void SetUpMap()
        {
            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }
        void InitializeLocationManager()
        {
            
           
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Coarse,
                PowerRequirement = Power.Medium
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            locationManager.RequestLocationUpdates(LocationManager.NetworkProvider,5000,5,this);
        }

        public void OnLocationChanged(Location location)
        {
            currentLocation = location; 
            
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
        
    }
}

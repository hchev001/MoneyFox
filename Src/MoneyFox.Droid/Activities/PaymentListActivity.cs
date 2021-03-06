using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Clans.Fab;
using MoneyFox.Business.ViewModels;
using MoneyFox.Droid.Fragments;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MoneyFox.Foundation.Resources;

namespace MoneyFox.Droid.Activities
{
    [Activity(Label = "PaymentListActivity",
        Name = "moneyfox.droid.activities.PaymentListActivity",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop)]
    public class PaymentListActivity : MvxAppCompatActivity<PaymentListViewModel>
    {
        private MvxExpandableListView paymentExpandable;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_payment_list);

            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            FindViewById<FloatingActionMenu>(Resource.Id.fab_menu_add_element).SetClosedOnTouchOutside(true);

            paymentExpandable = FindViewById<MvxExpandableListView>(Resource.Id.expandable_payment_list);
            if (paymentExpandable.Count > 0)
            {
                paymentExpandable.ExpandGroup(0);
            }
            RegisterForContextMenu(paymentExpandable);

            LoadBalancePanel();
            Title = ViewModel.Title;
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.expandable_payment_list)
            {
                menu.SetHeaderTitle(Strings.SelectOperationLabel);
                menu.Add(0, 0, 0, Strings.EditLabel);
                menu.Add(0, 1, 1, Strings.DeleteLabel);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var selected = ViewModel.RelatedPayments[ExpandableListView
                .GetPackedPositionChild(((
                    ExpandableListView.ExpandableListContextMenuInfo) item.MenuInfo)
                    .PackedPosition)];

            switch (item.ItemId)
            {
                case 0:
                    ViewModel.EditPaymentCommand.Execute(selected);
                    return true;

                case 1:
                    ViewModel.DeletePaymentCommand.Execute(selected);
                    return true;

                default:
                    return false;
            }
        }

        private void LoadBalancePanel()
        {
            var fragment = new BalanceFragment
            {
                ViewModel = (PaymentListBalanceViewModel) ViewModel.BalanceViewModel
            };

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.payment_list_balance_frame, fragment)
                .Commit();
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.LoadCommand.Execute();
        }

        /// <summary>
        ///     This hook is called whenever an item in your options menu is selected.
        /// </summary>
        /// <param name="item">The menu item that was selected.</param>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return false;
            }
        }
    }
}
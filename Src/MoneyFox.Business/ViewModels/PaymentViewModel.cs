﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MoneyFox.Foundation;
using MoneyFox.Service.Pocos;

namespace MoneyFox.Business.ViewModels
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     Default constructor. Will create a new <see cref="Payment"/>
        /// </summary>
        public PaymentViewModel()
        {
            Payment = new Payment();
        }

        /// <summary>
        ///     Constructor. Assignes the passed payment to the wrapped payment field.
        /// </summary>
        /// <param name="payment">Payment to use for further operations.</param>
        public PaymentViewModel(Payment payment)
        {
            Payment = payment;
        }

        public Payment Payment { get; set; }
        
        public int Id
        {
            get { return Payment.Data.Id; }
            set
            {
                if (Payment.Data.Id == value) return;
                Payment.Data.Id = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     In case it's a expense or transfer the foreign key to the <see cref="AccountViewModel" /> who will be charged.
        ///     In case it's an income the  foreign key to the <see cref="AccountViewModel" /> who will be credited.
        /// </summary>
        public int ChargedAccountId
        {
            get { return Payment.Data.ChargedAccountId; }
            set
            {
                if (Payment.Data.ChargedAccountId == value) return;
                Payment.Data.ChargedAccountId = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Foreign key to the account who will be credited by a transfer.
        ///     Not used for the other payment types.
        /// </summary>
        public int? TargetAccountId
        {
            get { return Payment.Data.TargetAccountId; }
            set
            {
                if (Payment.Data.TargetAccountId == value) return;
                Payment.Data.TargetAccountId = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Foreign key to the <see cref="Category" /> for this payment
        /// </summary>
        public int? CategoryId
        {
            get { return Payment.Data.CategoryId; }
            set
            {
                if (Payment.Data.CategoryId == value) return;
                Payment.Data.CategoryId = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Date when this payment will be executed.
        /// </summary>
        public DateTime Date
        {
            get { return Payment.Data.Date; }
            set
            {
                if (Payment.Data.Date == value) return;
                Payment.Data.Date = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Amount of the payment. Has to be >= 0. If the amount is charged or not is based on the payment type.
        /// </summary>
        public double Amount
        {
            get { return Payment.Data.Amount; }
            set
            {
                if (Math.Abs(Payment.Data.Amount - value) < 0.01) return;
                Payment.Data.Amount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Indicates if this payment was already executed and the amount already credited or charged to the respective
        ///     account.
        /// </summary>
        public bool IsCleared
        {
            get { return Payment.Data.IsCleared; }
            set
            {
                if (Payment.Data.IsCleared == value) return;
                Payment.Data.IsCleared = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Type of the payment <see cref="PaymentType" />.
        /// </summary>
        public PaymentType Type
        {
            get { return Payment.Data.Type; }
            set
            {
                if (Payment.Data.Type == value) return;
                Payment.Data.Type = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Additional notes to the payment.
        /// </summary>
        public string Note
        {
            get { return Payment.Data.Note; }
            set
            {
                if (Payment.Data.Note == value) return;
                Payment.Data.Note = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Indicates if the payment will be repeated or if it's a uniquie payment.
        /// </summary>
        public bool IsRecurring
        {
            get { return Payment.Data.IsRecurring; }
            set
            {
                if (Payment.Data.IsRecurring == value) return;
                Payment.Data.IsRecurring = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Foreign key to the <see cref="RecurringPayment" /> if it's recurring.
        /// </summary>
        public int? RecurringPaymentId
        {
            get { return Payment.Data.RecurringPaymentId; }
            set
            {
                if (Payment.Data.RecurringPaymentId == value) return;
                Payment.Data.RecurringPaymentId = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     In case it's a expense or transfer the account who will be charged.
        ///     In case it's an income the account who will be credited.
        /// </summary>
        public AccountViewModel ChargedAccount
        {
            get
            {
                return Payment.Data.ChargedAccount != null
                    ? new AccountViewModel(new Account(Payment.Data.ChargedAccount))
                    : null;
            }
            set
            {
                if (ChargedAccount == value) return;
                Payment.Data.ChargedAccount = value?.Account.Data;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     The <see cref="AccountViewModel" /> who will be credited by a transfer.
        ///     Not used for the other payment types.
        /// </summary>
        public AccountViewModel TargetAccount
        {
            get { return Payment.Data.TargetAccount != null
                ? new AccountViewModel(new Account(Payment.Data.TargetAccount))
                : null;
            }
            set
            {
                if (TargetAccount == value) return;
                Payment.Data.TargetAccount = value.Account.Data;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     The <see cref="Category" /> for this payment
        /// </summary>
        public CategoryViewModel Category
        {
            get { return new CategoryViewModel(new Category(Payment.Data.Category)); ; }
            set
            {
                if (Category == value) return;

                Payment.Data.Category = value.Category.Data;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     The <see cref="RecurringPayment" /> if it's recurring.
        /// </summary>
        public RecurringPaymentViewModel RecurringPayment
        {
            get { return new RecurringPaymentViewModel(new RecurringPayment(Payment.Data.RecurringPayment)); }
            set
            {
                if (RecurringPayment == value) return;

                Payment.Data.RecurringPayment = value.RecurringPayment.Data;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     This is a shortcut to access if the payment is a transfer or not.
        /// </summary>
        public bool IsTransfer => Type == PaymentType.Transfer;

        private int currentAccountId;

        public int CurrentAccountId
        {
            get { return currentAccountId; }
            set
            {
                if (currentAccountId == value) return;
                currentAccountId = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
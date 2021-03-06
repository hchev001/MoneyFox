﻿using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;

namespace MoneyFox.Business.Tests.Mocks
{
    public class MockDispatcher
        : MvxMainThreadDispatcher
            , IMvxViewDispatcher
    {
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            Requests.Add(request);
            return true;
        }

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return true;
        }
    }
}
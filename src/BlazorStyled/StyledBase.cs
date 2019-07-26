﻿using BlazorStyled.Stylesheets;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorStyled
{
    public class StyledBase : ComponentBase, IObserver<StyleSheet>, IDisposable
    {
        [Inject] protected StyleSheet StyleSheet { get; private set; }
        private string _hashCode = null;

        private IDisposable _unsubscriber;

        protected override void OnInit()
        {
            _unsubscriber = StyleSheet.Subscribe(this);
        }

        public void OnCompleted()
        {
            _unsubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(StyleSheet value)
        {
            //Only call state has changed if the stylesheet really changed
            string newHashCode = StyleSheet.GetHashCodes();
            if (_hashCode != newHashCode)
            {
                StateHasChanged();
                _hashCode = newHashCode;
            }
        }

        public void Dispose()
        {
            _unsubscriber.Dispose();
        }
    }
}

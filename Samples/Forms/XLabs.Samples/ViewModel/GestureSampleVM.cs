// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="GestureSampleVM.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System.Collections.ObjectModel;
using Xamarin.Forms;
using XLabs.Forms.Behaviors;
using XLabs.Forms.Controls;

namespace XLabs.Samples.ViewModel
{
    class GestureSampleVm : BaseViewModel
    {
        private int _gesturecount;
        private bool _excludechildren = true;
        private string _frametext = "This frame accepts gestures\nThe enclosed Label does not", _labeltext="I do nothing";

        public GestureSampleVm()
        {
            Gestures=new ObservableCollection<string>();
            DumpGesture=new RelayGesture(OnGesture);
            ClearGestures=new Command(_=>Gestures.Clear());
            ToggleChildren=new Command(_ =>
                {
                    ExcludeChildren = !ExcludeChildren;
                    if (ExcludeChildren)
                    {
                        FrameText = "This frame accepts gestures\nThe enclosed Label does not";
                        LabelText = "I do nothing";
                    }
                    else
                    {
                        FrameText = "This frame accepts gestures\nThe enclodes label does as well.";
                        LabelText = "I accept gestures";                        
                    }
                });

        }

        private void OnGesture(GestureResult gr, object obj)
        {
            GestureCount++;
            switch (gr.GestureType)
            {
                case GestureType.SingleTap:
                    Gestures.Insert(0, string.Format("Gesture:{0} param is {1}", gr.GestureType, obj));
                    break;
                case GestureType.DoubleTap:
                    Gestures.Insert(0, string.Format("Gesture:{0} param is {1}", gr.GestureType, obj));
                    break;
                case GestureType.LongPress:
                    Gestures.Insert(0, string.Format("Gesture:{0} param is {1}", gr.GestureType, obj));
                    break;
                case GestureType.Swipe:
                    Gestures.Insert(0,string.Format("Gesture:{0} Direction: {1} param is {2}",gr.GestureType,gr.Direction,obj));
                    break;
            }            
        }
        public string FrameText { get { return _frametext; } set { SetField( ref _frametext,value);} }
        public string LabelText { get { return _labeltext; } set { SetField(ref _labeltext, value); } }
        public bool ExcludeChildren { get { return _excludechildren; } set { SetField(ref _excludechildren, value); } }
        public int GestureCount { get { return _gesturecount; } private set { SetField(ref _gesturecount, value); } }
        public Command ClearGestures { get; private set; }
        public Command ToggleChildren { get; private set; }
        public RelayGesture DumpGesture { get; set; }
        public ObservableCollection<string> Gestures { get; private set; }
    }
}

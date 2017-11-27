﻿using System;
using Java.IO;
using Java.Lang;
using Android.Hardware.Camera2;
using WeldingMask.Droid.Abstractions;
using WeldingMask.Droid.Renderers;

namespace WeldingMask.Droid.Listeners
{
    public class CameraCaptureListener : CameraCaptureSession.CaptureCallback
    {
        public CameraViewRenderer Owner { get; set; }
        public File File { get; set; }
        public override void OnCaptureCompleted(CameraCaptureSession session, CaptureRequest request, TotalCaptureResult result)
        {
            Process(result);
        }

        public override void OnCaptureProgressed(CameraCaptureSession session, CaptureRequest request, CaptureResult partialResult)
        {
            Process(partialResult);
        }

        private void Process(CaptureResult result)
        {
            switch (Owner.mState)
            {
                case CameraViewRenderer.STATE_WAITING_LOCK:
                    {
                        Integer afState = (Integer)result.Get(CaptureResult.ControlAfState);
                        if (afState == null)
                        {
                            Owner.CaptureStillPicture();
                        }

                        else if ((((int)ControlAFState.FocusedLocked) == afState.IntValue()) ||
                                   (((int)ControlAFState.NotFocusedLocked) == afState.IntValue()))
                        {
                            // ControlAeState can be null on some devices
                            Integer aeState = (Integer)result.Get(CaptureResult.ControlAeState);
                            if (aeState == null ||
                                    aeState.IntValue() == ((int)ControlAEState.Converged))
                            {
                                Owner.mState = CameraViewRenderer.STATE_PICTURE_TAKEN;
                                Owner.CaptureStillPicture();
                            }
                            else
                            {
                                Owner.RunPrecaptureSequence();
                            }
                        }
                        break;
                    }
                case CameraViewRenderer.STATE_WAITING_PRECAPTURE:
                    {
                        // ControlAeState can be null on some devices
                        Integer aeState = (Integer)result.Get(CaptureResult.ControlAeState);
                        if (aeState == null ||
                                aeState.IntValue() == ((int)ControlAEState.Precapture) ||
                                aeState.IntValue() == ((int)ControlAEState.FlashRequired))
                        {
                            Owner.mState = CameraViewRenderer.STATE_WAITING_NON_PRECAPTURE;
                        }
                        break;
                    }
                case CameraViewRenderer.STATE_WAITING_NON_PRECAPTURE:
                    {
                        // ControlAeState can be null on some devices
                        Integer aeState = (Integer)result.Get(CaptureResult.ControlAeState);
                        if (aeState == null || aeState.IntValue() != ((int)ControlAEState.Precapture))
                        {
                            Owner.mState = CameraViewRenderer.STATE_PICTURE_TAKEN;
                            Owner.CaptureStillPicture();
                        }
                        break;
                    }
            }
        }
    }
}

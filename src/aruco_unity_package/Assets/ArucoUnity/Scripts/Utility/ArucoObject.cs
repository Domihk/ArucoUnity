﻿using ArucoUnity.Plugin;
using UnityEngine;

namespace ArucoUnity
{
  /// \addtogroup aruco_unity_package
  /// \{

  namespace Utility
  {
    public abstract class ArucoObject : MonoBehaviour
    {
      // Editor fields
      [SerializeField]
      private ArucoObjectController arucoObjectController;

      [SerializeField]
      private PREDEFINED_DICTIONARY_NAME dictionaryName;

      [SerializeField]
      private float markerSideLength;

      [SerializeField]
      public int marginsSize;

      [SerializeField]
      public int markerBorderBits;

      // Properties

      public ArucoObjectController ArucoObjectController
      {
        get { return arucoObjectController; }
        set
        {
          ArucoObjectController previousArucoObjectController = arucoObjectController;
          arucoObjectController = value;

          if (previousArucoObjectController != null)
          {
            previousArucoObjectController.Remove(this);
          }
          if (arucoObjectController != null)
          {
            ArucoObjectController.Add(this);
          }
        }
      }

      public Dictionary Dictionary { get; set; }

      public float MarkerSideLength 
      { 
        get { return markerSideLength; }
        set
        {
          float previousMarkerSideLength = markerSideLength;
          markerSideLength = value;

          if (previousMarkerSideLength != 0)
          {
            transform.localScale /= previousMarkerSideLength;
          }

          if (markerSideLength != 0)
          {
            transform.localScale *= markerSideLength;
          }
        }
      }

      public int MarginsSize { get { return marginsSize; } set { marginsSize = value; } }

      public int MarkerBorderBits { get { return markerBorderBits; } set { markerBorderBits = value; } }

      // MonoBehaviour methods

      protected void Awake()
      {
        Dictionary = Functions.GetPredefinedDictionary(dictionaryName);
      }
    }
  }

  /// \} aruco_unity_package
}
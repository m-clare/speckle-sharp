﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSiAPIv1;

namespace DriverCSharp
{
  class Program
  {
    private const string ProgID_SAP2000 = "CSI.SAP2000.API.SapObject";
    private const string ProgID_ETABS = "CSI.ETABS.API.ETABSObject";

    static int Main(string[] args)
    {
      //MessageBox.Show("Starting DriverCSharp");

      // dimension the SapObject as cOAPI type
      cOAPI mySapObject = null;

      // Use ret to check if functions return successfully (ret = 0) or fail (ret = nonzero)
      int ret = -1;

      // create API helper object
      cHelper myHelper = null;

      try
      {
        myHelper = new Helper();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Cannot create an instance of the Helper object: " + ex.Message);
        ret = -1;
        return ret;
      }

      // attach to a running program instance 
      try
      {
        // get the active SapObject
        // determine program type
        string progID = null;
        string[] arguments = Environment.GetCommandLineArgs();

        if (arguments.Count() > 1)
        {
          string arg = arguments[1];
          if (string.Compare(arg, "SAP2000", true) == 0)
            progID = ProgID_SAP2000;
          else if (string.Compare(arg, "ETABS", true) == 0)
            progID = ProgID_ETABS;
        }

        if (progID != null)
          mySapObject = myHelper.GetObject(progID);
        else
        {
          // missing/unknown program type, try one by one
          try
          {
            progID = ProgID_SAP2000;
            mySapObject = myHelper.GetObject(progID);
          }
          catch (Exception ex)
          {
          }

          if (mySapObject == null)
          {
            progID = ProgID_ETABS;
            mySapObject = myHelper.GetObject(progID);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("No running instance of the program found or failed to attach: " + ex.Message);

        ret = -2;
        return ret;
      }

      // Get a reference to cSapModel to access all API classes and functions
      cSapModel mySapModel = mySapObject.SapModel;

      // call Speckle plugin
      try
      {
        SpeckleConnectorETABS.cPlugin p = new SpeckleConnectorETABS.cPlugin();
        cPluginCallback cb = new PluginCallback();

        // DO NOT return from SpeckleConnectorETABS.cPlugin.Main() until all work is done.
        p.Main(ref mySapModel, ref cb);

        return cb.ErrorFlag;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to call plugin: " + ex.Message);

        ret = -3;
        return ret;
      }
    }
  }
}
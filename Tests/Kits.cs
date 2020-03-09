﻿using System;
using Speckle.Kits;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
  public class Kits
  {
    [Test]
    public void KitsExist()
    {
      var kits = KitManager.Kits;
      Assert.Greater(kits.Count(), 0);

      var types = KitManager.Types;
      Assert.Greater(types.Count(), 0);
    }
  }
}

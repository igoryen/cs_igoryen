using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using igoryen.ViewModels;
using igoryen.Models;

namespace igoryen.ViewModels {
  public class RepositoryBase {

    protected DataContext dc;

    //====================================================================
    // the class's constructor
    // 20. 20. Turn off the Entity Framework (EF) proxy creation features
    //   We do NOT want the EF to track changes - we'll do that ourselves
    // 25. Also, turn off lazy loading...
    //   We want to retain control over fetching related objects
    //====================================================================
    public RepositoryBase() {

      dc = new DataContext();

      dc.Configuration.ProxyCreationEnabled = false; // 20
      dc.Configuration.LazyLoadingEnabled = false; // 25
    }

  }
}
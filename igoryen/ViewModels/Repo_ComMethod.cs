using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using igoryen.Models;

namespace igoryen.ViewModels {
  public class Repo_ComMethod : RepositoryBase {

    //======================================
    // getCourseSelectList()
    //======================================
    public SelectList getComMethodSelectList() {
      SelectList sl = new SelectList(getListOfComMethodAM(), "ComMethodId");
      return sl;
    }

    //======================================
    // getListOfComMethodAM() - with automapper
    //====================================== 
    public IEnumerable<ComMethod> getListOfComMethodAM() {
      var cmethods = dc.ComMethods.OrderBy(cm => cm.ComMethodId);
      if (cmethods == null) return null;
      return Mapper.Map<IEnumerable<ComMethod>>(cmethods);
    }

    //======================================
    // getListOfComMethod() 
    //====================================== 
    public static List<ComMethod> getListOfComMethod(List<igoryen.Models.ComMethod> ls) {
      List<ComMethod> nls = new List<ComMethod>();

      foreach (var item in ls) {
        ComMethod cm = new ComMethod();
        cm.ComMethodId = item.ComMethodId;
        cm.Handle = item.Handle;
        cm.CellNo = item.CellNo;
        cm.Email = item.Email;
        nls.Add(cm);
      }

      return nls;
    }

    // T

    //======================================
    // toListOfComMethod()
    //====================================== 
    public List<ComMethod> toListOfComMethod(List<Models.ComMethod> cmethods) {
      List<ComMethod> lcm = new List<ComMethod>();
      foreach (var item in cmethods) {
        ComMethod cm = new ComMethod();
        cm.ComMethodId = item.ComMethodId;
        cm.Handle = item.Handle;
        cm.CellNo = item.CellNo;
        cm.Email = item.CellNo;
        lcm.Add(cm);
      }
      return lcm;
    }

  }
}
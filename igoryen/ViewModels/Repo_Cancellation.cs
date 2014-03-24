using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace igoryen.ViewModels {
  public class Repo_Cancellation : RepositoryBase {
    // methods alphabetically

    // C

    //======================================
    // CreateCancellation() - with Automapper
    // 50. nulls are like time bombs
    //======================================
    /*
    public CancellationFull createCancellationAM(ViewModels.CancellationCreate newItem, string d) {
      Models.Cancellation cancellation = Mapper.Map<Models.Cancellation>(newItem);
      int did = Convert.ToInt32(d);
      cancellation.Faculty = dc.Faculties.FirstOrDefault(n => n.Id == did);

      if (cancellation.Faculty == null) return null; // 50

      dc.Cancellations.Add(cancellation);
      dc.SaveChanges();

      return Mapper.Map<CancellationFull>(cancellation);
    }
     */

    // D 

    //======================================
    // DeleteCancellation()
    // 20. return [void] since the function's retval is void
    //======================================
    public void DeleteCancellation(int? id) {
      var itemToDelete = dc.Cancellations.Find(id);
      if (itemToDelete == null) {
        return; // 20
      } // if
      else {
        try {
          dc.Cancellations.Remove(itemToDelete);
          dc.SaveChanges();
        }
        catch (Exception ex) {
          throw ex;
        }
      } // else
    } // DeleteCancellation()

    // E

    //======================================
    // EditCancellation() - with Automapper
    //======================================
    public CancellationFull editCancellationAM(CancellationFull editItem) {
      var itemToEdit = dc.Cancellations.Find(editItem.CancellationId);
      if (itemToEdit == null) {
        return null;
      }
      else {
        dc.Entry(itemToEdit).CurrentValues.SetValues(editItem);
        dc.SaveChanges();
      }
      return Mapper.Map<CancellationFull>(editItem);
    }


    // G

    //======================================
    // getCancellationFull() - with Automapper
    //====================================== 
    public CancellationFull getCancellationFullAM(int? id) {
      var cancellation = dc.Cancellations.Include("Faculty").SingleOrDefault(n => n.CancellationId == id);
      if (cancellation == null) return null;
      else return Mapper.Map<CancellationFull>(cancellation);
    }

    //======================================
    // getListOfCancellationBase() - with automapper
    //====================================== 
    public IEnumerable<CancellationBase> getListOfCancellationBaseAM() {
      var cancellations = dc.Cancellations.OrderBy(c => c.CancellationId);
      if (cancellations == null) return null;
      return Mapper.Map<IEnumerable<CancellationBase>>(cancellations);
    }

    //======================================
    // getListOfCancellationBase() 
    //====================================== 
    /*
    public static List<CancellationBase> getListOfCancellationBase(List<igoryen.Models.Cancellation> ls) {
      List<CancellationBase> lcb = new List<CancellationBase>();

      foreach (var item in ls) {
        CancellationBase cb = new CancellationBase();
        cb.CancellationId = item.CancellationId;
        cb.Date = item.Date;
        lcb.Add(cb);
      }

      return lcb;
    }*/

    // I

    //======================================
    // Implementation details
    //======================================
    Repo_Faculty rf;
    Repo_Course rc;


    // R

    //======================================
    // Repo_Course() - Constructor
    //======================================
    public Repo_Cancellation() {
      rf = new Repo_Faculty();
      rc = new Repo_Course();
    }

    // T

    //======================================
    // toListOfCancellationBase()
    //======================================
    /*
    public List<CancellationBase> toListOfCancellationBase(List<Models.Cancellation> cancellations) {
      List<CancellationBase> lcb = new List<CancellationBase>();
      foreach (var item in cancellations) {
        CancellationBase cb = new CancellationBase();
        cb.CancellationId = item.CancellationId;
        cb.Date = item.Date;
        lcb.Add(cb);
      }
      return lcb;
    }*/
  }
}
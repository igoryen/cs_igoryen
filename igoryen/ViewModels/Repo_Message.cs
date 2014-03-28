using igoryen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace igoryen.ViewModels {
  public class Repo_Message : RepositoryBase {

    // methods alphabetically

    // C

    //======================================
    // CreateMessage() - with Automapper
    // 50. nulls are like time bombs
    //======================================
    public MessageFull createMessageAM(ViewModels.MessageCreate newItem, string d) {
      Models.Message message = Mapper.Map<Models.Message>(newItem);
      int did = Convert.ToInt32(d);
      message.Faculty = dc.Faculties.FirstOrDefault(n => n.PersonId == did);

      if (message.Faculty == null) return null; // 50

      dc.Messages.Add(message);
      dc.SaveChanges();

      return Mapper.Map<MessageFull>(message);
    }

    // D 

    //======================================
    // DeleteMessage()
    // 20. return [void] since the function's retval is void
    //======================================
    public void DeleteMessage(int? id) {
      var itemToDelete = dc.Messages.Find(id);
      if (itemToDelete == null) {
        return; // 20
      } // if
      else {
        try {
          dc.Messages.Remove(itemToDelete);
          dc.SaveChanges();
        }
        catch (Exception ex) {
          throw ex;
        }
      } // else
    } // DeleteMessage()

    // E

    //======================================
    // EditMessage() - with Automapper
    //======================================
    public MessageFull editMessageAM(MessageFull editItem) {
      var itemToEdit = dc.Messages.Find(editItem.MessageId);
      if (itemToEdit == null) {
        return null;
      }
      else {
        dc.Entry(itemToEdit).CurrentValues.SetValues(editItem);
        dc.SaveChanges();
      }
      return Mapper.Map<MessageFull>(editItem);
    }


    // G

    //======================================
    // getListOfMessageBase() 
    //====================================== 

    public static List<MessageBase> getListOfMessageBase(List<igoryen.Models.Message> ls) {
      List<MessageBase> nls = new List<MessageBase>();

      foreach (var item in ls) {
        MessageBase row = new MessageBase();
        row.MessageId = item.MessageId;
        row.Date = item.Date;
        row.CourseName = item.CourseName;
        row.Body = item.Body;
        nls.Add(row);
      }

      return nls;
    }

    //======================================
    // getListOfMessageBaseAM() - with automapper
    //====================================== 
    public IEnumerable<MessageBase> getListOfMessageBaseAM() {
      var messages = dc.Messages.OrderBy(m => m.MessageId);
      if (messages == null) return null;
      return Mapper.Map<IEnumerable<MessageBase>>(messages);
    }

    //======================================
    // getMessageFullAM() - with Automapper
    //====================================== 
    public MessageFull getMessageFullAM(int? id) {
      var message = dc.Messages.Include("Faculty").SingleOrDefault(m => m.MessageId == id);
      if (message == null) return null;
      else return Mapper.Map<MessageFull>(message);
    }
    

    //======================================
    // getMessageSelectList()
    //======================================
    public SelectList getMessageSelectList() {
      SelectList sl = new SelectList(getListOfMessageBaseAM(), "MessageId", "Body");
      return sl;
    }

    // I

    //======================================
    // Implementation details
    //======================================
    Repo_Faculty rf;
    Repo_Student rs;


    // R

    //======================================
    // Repo_Message() - Constructor
    //======================================
    public Repo_Message() {
      rf = new Repo_Faculty();
      rs = new Repo_Student();
    }

    // T

    //======================================
    // toListOfMessageBase()
    //====================================== 
    public List<MessageBase> toListOfMessageBase(List<Models.Message> messages) {
      List<MessageBase> lmb = new List<MessageBase>();
      foreach (var item in messages) {
        MessageBase mb = new MessageBase();
        mb.MessageId = item.MessageId;
        mb.Date = item.Date;
        mb.CourseName = item.CourseName;
        mb.Body = mb.Body;
        lmb.Add(mb);
      }
      return lmb;
    }

    public List<Message> Messages { get; set; }
  }
}
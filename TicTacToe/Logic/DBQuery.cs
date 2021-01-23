using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;

namespace Logic
{
    public class DBQuery
    {
        public static List<User> GetListOfUsers()
        {
            using (TicTacToeDBEntities db = new TicTacToeDBEntities())
            {
                try
                {
                    var users = db.Users.OrderByDescending(u => u.CountOfWin).Take(100).ToList();
                    return users;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static List<Dimension> GetListOfDimensions()
        {
            using (TicTacToeDBEntities db = new TicTacToeDBEntities())
            {
                try
                {
                    var dimensions = db.Dimensions.ToList();
                    return dimensions;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static User Authorization(User currentUser)
        {
            using (TicTacToeDBEntities db = new TicTacToeDBEntities())
            {
                try
                {
                    var user = db.Users.Where(u => u.Login == currentUser.Login && u.Password == currentUser.Password).FirstOrDefault();
                    if (user == null)
                        return new User();
                    else
                        return user;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static Exception IncrementCountOfWin(User currentUser)
        {
            using (TicTacToeDBEntities db = new TicTacToeDBEntities())
            {
                try
                {
                    var user = db.Users.Where(u => u.Login == currentUser.Login).FirstOrDefault();
                    user.CountOfWin++;
                    db.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
        }

        public static Exception AddUser(User user)
        {
            using (TicTacToeDBEntities db = new TicTacToeDBEntities())
            {
                try
                {
                    var exist = db.Users.Where(u => u.Login == user.Login).FirstOrDefault();
                    if (exist == null)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        return null;
                    }
                    else
                        return new Exception();
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
        }
    }
}

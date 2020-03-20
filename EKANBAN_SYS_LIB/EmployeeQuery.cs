using EKANBAN_SYS_LIB.InterfaceQuery;
using EMPLOYEE;
using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKANBAN_SYS_LIB
{
    public class EmployeeQuery : IEmployeeQuery
    {
        private IDbName database;
        public EmployeeContext EmployeeContext { get; set; }
        public EmployeeQuery(IDbName _database)
        {
            database = _database;
        }
        public bool AddAppUser(AppUser _user)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.AppUsers.Add(_user);
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddDepartment(Department _department)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Departments.Add(_department);
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddJobTitle(JobTitle _jobtile)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.JobTitles.Add(_jobtile);
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddNewEmployee(Employee _employee)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Employees.Add(_employee);
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool AddPosition(Position _postion)
        {
            throw new NotImplementedException();
        }

        public AppUser GetAppUser(string _userName, string _passWord)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    return EmployeeContext.AppUsers
                        .Where(i => i.user == _userName && i.password == _passWord)
                        .First();
                }
            }
            catch { return null; }
        }

        public AppUser GetAppUser(int _userId)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    return EmployeeContext.AppUsers.Find(_userId);
                }
            }
            catch { return null; }
        }

        public Employee GetEmployee(int _employeeId)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    return EmployeeContext.Employees
                       .Include("Department")
                       .Include("Position")
                       .Include("JobTitle")
                       .Where(i => i.id == _employeeId || i.UserCode == _employeeId)
                       .First();
                }
            }
            catch { return null; }
        }

        public Employee GetEmployee(string _rfidNumber)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    return EmployeeContext.Employees
                        .Include("Department")
                        .Include("Position")
                        .Include("JobTitle")
                        .Where(i => i.RFID_Code == _rfidNumber)
                        .First();
                }
            }
            catch { return null; }
        }

        public ICollection<Employee> GetEmployee(Department _department)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    var department = EmployeeContext.Departments
                        .Include("Employees")
                        .Where(i => i.id == _department.id)
                        .First();
                    return department.Employees;
                }
            }
            catch { return null; }
        }

        public ICollection<Employee> GetEmployee(Position _position)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    var position = EmployeeContext.Positions
                        .Include("Employees")
                        .Where(i => i.id == _position.id).First();
                    return position.Employees;
                }
            }
            catch { return null; }
        }

        public ICollection<Employee> GetEmployee(JobTitle _jobtile)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    var jobtile = EmployeeContext.JobTitles
                        .Include("")
                        .Where(i => i.id == _jobtile.id)
                        .First();
                    return jobtile.Employees;
                }
            }
            catch { return null; }
        }

        public ICollection<Employee> GetEmployees(int _buidingId)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    return EmployeeContext.Employees
                        .Where(i => i.Building_Id == _buidingId)
                        .ToList();
                }
            }
            catch { return null; }
        }

        public bool UpdateAppUser(AppUser _appUser)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Entry(_appUser).State = EntityState.Modified;
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateDepartment(Department _department)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Entry(_department).State = EntityState.Modified;
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateEmployee(Employee _employee)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Entry(_employee).State = EntityState.Modified;
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdateJobtile(JobTitle _jobtitle)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Entry(_jobtitle).State = EntityState.Modified;
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool UpdatePostion(Position _postion)
        {
            try
            {
                using (EmployeeContext = new EmployeeContext(database))
                {
                    EmployeeContext.Entry(_postion).State = EntityState.Modified;
                    EmployeeContext.SaveChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public Department AddNewDepartment(string dep)
        {
            using (EmployeeContext = new EmployeeContext(database))
            {
                try
                {
                    var Dep = EmployeeContext.Departments.Add(new Department
                    {
                        Code = dep,
                    });
                    EmployeeContext.SaveChanges();
                    return Dep;
                }
                catch { return null; }
            }
        }
        public Department GetDepartment(string dep)
        {
            using (EmployeeContext = new EmployeeContext(database))
            {
                try
                {
                    return EmployeeContext.Departments.Where(i => i.Code == dep.Trim()).First();
                }
                catch { return null; }
            }
        }

        public Employee AddNewBeamWorker(Employee worker)
        {
            using (EmployeeContext = new EmployeeContext(database))
            {
                try
                {
                    var Emp = EmployeeContext.Employees.Add(worker);
                    EmployeeContext.SaveChanges();
                    return Emp;
                }
                catch { return null; }
            }
        }
    }
}

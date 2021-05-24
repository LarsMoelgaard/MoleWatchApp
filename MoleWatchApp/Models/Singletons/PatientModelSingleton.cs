using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class PatientModelSingleton
    {
        private static IPatientModel _patientModel;

        private static IPatientModel patientModel
        {
            get
            {
                if (_patientModel == null)
                {
                    _patientModel = new PatientModelModel(); 
                }

                return _patientModel;
            }
        }

        public static IPatientModel GetPatientModel()
        {
            return patientModel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataClasses.DTO
{
    public class MedicalPracticePatientsDTO
    {
        public int MedicalPracticeID { get; set; }
        public List<PatientInfoDTO> PatientList { get; set; }
    }
}

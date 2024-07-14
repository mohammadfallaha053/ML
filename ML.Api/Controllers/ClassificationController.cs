using Microsoft.AspNetCore.Mvc;
using ML.Api.Dtos.Classification;
namespace ML_API.Controllers
{
    [ApiController]
    [Route("api/v2/Classification")]
    public class ClassificationController : ControllerBase
    {
        [HttpPost("HeartAttack")]
        public IActionResult GetPredictionHeartAttack([FromBody] HeartAttackDto dto)
        {
            var classfiction = new DecisionTreePredictor();
            var resualt = classfiction.PredictHeartAttack(dto.Troponin,dto.Glucose,dto.Impluse,dto.Gender,dto.Age);
            return Ok(resualt);
        }

        [HttpPost("CKD")]
        public IActionResult GetPredictionCKD([FromBody] CKDDto dto)
        {
            var classfiction = new DecisionTreePredictor();
            var resualt = classfiction.PredictCKD(dto.SerumCreatinine, dto.Potassium, dto.Hemoglobin, dto.DiabetesMellitus, dto.Appetite, dto.Age, dto.BloodUrea);
            return Ok(resualt);
        }

        [HttpPost("Diabetes")]
        public IActionResult GetPredictionDiabetes([FromBody] DiabetesDto dto)
        {
            var classfiction = new DecisionTreePredictor();
            var resualt = classfiction.PredictDiabetes(dto.Glucose,dto.HbA1cLevel,dto.HeartDisease,dto.HeartDisease,dto.Age,dto.Gender);
            return Ok(resualt);
        }
    }

    public  class  DecisionTreePredictor
    {
        public bool PredictCKD(double? serumCreatinine, double? potassium, double? hemoglobin, bool? diabetesMellitus, bool? appetite, int? age, int? bloodUrea)

        {
            //if (serumCreatinine == null || diabetesMellitus == null || hemoglobin == null) return "Can't PredictCKD there are some data rquired";

            
                if (serumCreatinine > 12) return true;

                else
                {
                    if (diabetesMellitus == true) return true;

                    else
                    {
                        if (hemoglobin <= 12.9) return true;

                        else return false;
                    }
                }
            
        }
        public  bool PredictHeartAttack(double? troponin, double? glucose,int? impluse, int? gender, int? age)
        {
            //if (troponin == null) return "Can't PredictHeartAttack there are some data rquired";

            
                if (troponin <= 0.014)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            
        }
        public bool PredictDiabetes(double? glucose, double? hbA1cLevel, int? Hypertension, int? heartDisease, int? age, int? gender)
        {
            //if (hbA1cLevel == null || glucose == null ) return "Can't PredictDiabetes there are some data rquired";

            

            if (hbA1cLevel >6.6)
            {
                return true;
            }
            else
            {
               if(glucose>200)
               {
                    return false;
               }
                else
                {
                    return false;
                }

            }
        }

    }     

 

   



  

}

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IdentityType
    {
         None = 0,
        /// <summary>
        /// 身份证
        /// </summary>
         IdentityCard = 1,
        /// <summary>
        /// 护照
        /// </summary>
        Passport = 2,
        /// <summary>
        /// 军官证
        /// </summary>
        OfficersCertificate = 3,
        /// <summary>
        /// 执业医师资格证
        /// </summary>
        MedicalCertificate = 5,
        /// <summary>
        /// 执业助理医师资格证
        /// </summary>
        AssistantMedicalCertificate = 6,
        /// <summary>
        /// 执业药师资格证
        /// </summary>
        PharmacistCertificate = 7,
        /// <summary>
        /// 执业技师资格证
        /// </summary>
        ProfessionalEngineerCertificate = 8,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        HkOrMacaoPassport = 11,
        /// <summary>
        ///市民卡
        /// </summary>
        CitizensCardNumber = 12,
        /// <summary>
        ///  社保卡号
        /// </summary>
        SocialCardNumber = 13,
        /// <summary>
        /// 医院诊疗卡
        /// </summary>
        MedicalInsuranceNumber = 14
    }
}

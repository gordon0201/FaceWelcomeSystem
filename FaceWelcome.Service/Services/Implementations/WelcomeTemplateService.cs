using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Request.WelcomeTemplate;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Service.DTOs.Response.WelcomeTemplate;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class WelcomeTemplateService : IWelcomeTemplateService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public WelcomeTemplateService(IUnitOfWork unitOfWork, IMapper mapper)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        #region Get Template By GuestId
        public async Task<GetTemplateResponse> GetTemplateByGuestIdAsync(Guid guestId)
        {
            try
            {
                var guest = await _unitOfWork.GuestRepository.GetByIdAsync(guestId);
                if (guest == null)
                {
                    throw new Exception("Guest cannot found");
                }
                var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(guest.GroupId);
                if (group == null)
                {
                    throw new Exception("Group cannot found");
                }
                var template = await _unitOfWork.WelComeTemplateRepository.GetTemplateByIdAsync(group.WelcomeId);

                var templateResponse = this._mapper.Map<GetTemplateResponse>(template);
                return templateResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Get Welcome Template By Id
        public async Task<GetTemplateResponse> GetTemplateByIdAsync(Guid id)
        {
            try
            {
                var template = await _unitOfWork.WelComeTemplateRepository.GetTemplateByIdAsync(id);
                if (template == null)
                {
                    throw new Exception("Template cannot found");
                }
                var templateResponse = this._mapper.Map<GetTemplateResponse>(template);
                return templateResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get All WelcomeTemplates
        public async Task<GetTemplatesResponse> GetAllTemplatesAsync(GetTemplatesRequest getTemplatesRequest)
        {
            var templates = await _unitOfWork.WelComeTemplateRepository.GetAllWelcomeTemplatesAsync();
            int totalRecords = templates.Count;
            int totalPages = (int)System.Math.Ceiling((double)totalRecords / getTemplatesRequest.PageSize);

            if (templates == null)
            {
                throw new Exception("Templates are not found.");
            }

            try
            {
                var list = templates.Skip((getTemplatesRequest.PageNumber - 1) * getTemplatesRequest.PageSize)
               .Take(getTemplatesRequest.PageSize)
               .ToList();
                var responseList = this._mapper.Map<List<GetTemplateResponse>>(list);

                var response = new GetTemplatesResponse
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    PageNumber = getTemplatesRequest.PageNumber,
                    PageSize = getTemplatesRequest.PageSize,
                    Templates = responseList
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

    }
}

using FaceWelcome.Repository.Infrastructures;
using AutoMapper;

using FaceWelcome.Service.DTOs.Request.Organization;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Response.Organization;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Response.Person;

namespace FaceWelcome.Service.Services.Implementations
{
    public class OrganizationService : IOrganizationService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        #region Create organization
        public async Task CreateOrganizationAsync(PostOrganizationRequest postOrganizationRequest)
        {
            var organization = await _unitOfWork.OrganizationRepository
                .GetOrganizationByCodeAsync(postOrganizationRequest.Code);
            var orgGroup = await _unitOfWork.OrganizationGroupRepository
                .GetOrganizationGroupByIdAsync(postOrganizationRequest.OrganizationGroupId);
            if (organization != null)
            {
                throw new Exception("Code was existed.");
            }
            if (orgGroup == null)
            {
                throw new Exception("Cannot found the Organization Group");
            }
            var org = new Organization
            {
                City = postOrganizationRequest.City,
                Code = postOrganizationRequest.Code,
                District = postOrganizationRequest.District,
                Email = postOrganizationRequest.Email,
                Name = postOrganizationRequest.Name,
                OrganizationGroupId = postOrganizationRequest.OrganizationGroupId,
                Province = postOrganizationRequest.Province,
                Status = postOrganizationRequest.Status.ToString(),
            };
            await _unitOfWork.OrganizationRepository.AddAsync(org);
            await _unitOfWork.CommitAsync();

        }
        #endregion

        #region Get all organizations
        public async Task<GetOrgsResponse> GetAllOrganizations(GetOrganizationsRequest getOrganizationsRequest)
        {
            var orgs = await _unitOfWork.OrganizationRepository.GetOrganizationsAsync();
            if (orgs == null)
            {
                throw new Exception("Cannot found any results");
            }
            try
            {
                int totalRecords = orgs.Count;
                int totalPages = (int)System.Math.Ceiling((double)totalRecords / getOrganizationsRequest.PageSize);
                var list = orgs.Skip((getOrganizationsRequest.PageNumber - 1) * getOrganizationsRequest.PageSize)
                   .Take(getOrganizationsRequest.PageSize)
                   .ToList();
                var responseList = this._mapper.Map<List<GetOrgResponse>>(list);

                var response = new GetOrgsResponse
                {
                    PageNumber = getOrganizationsRequest.PageNumber,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    PageSize = getOrganizationsRequest.PageSize,
                    getOrgResponses = responseList
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Get By Id
        public async Task<GetOrgResponse> GetOrgByIdAsync(Guid id)
        {
            try
            {
                var org = await _unitOfWork.OrganizationRepository.GetOrganizationByIdAsync(id);
                if (org == null)
                {
                    throw new Exception("Organization cannot found");
                }
                var response = this._mapper.Map<GetOrgResponse>(org);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get by code
        public async Task<Organization> GetOrganizationByCodeAsync(string code)
        {
            try
            {
                var organization = await _unitOfWork.OrganizationRepository.GetOrganizationByCodeAsync(code);
                if (organization == null)
                {
                    throw new Exception("The code is wrong or invalid");
                }
                return organization;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

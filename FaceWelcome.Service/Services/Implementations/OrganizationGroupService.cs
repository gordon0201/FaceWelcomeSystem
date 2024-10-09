using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
using FaceWelcome.Service.DTOs.Response.OrganizationGroup;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class OrganizationGroupService : IOrganizationGroupService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public OrganizationGroupService(IUnitOfWork unitOfWork, IMapper mapper)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        public async Task CreateOrganizationGroupAsync(PostOrganizationGroupRequest postOrganizationGroupRequest)
        {
            if (postOrganizationGroupRequest == null)
            {
                throw new ArgumentNullException(nameof(postOrganizationGroupRequest), "Request cannot be null");
            }

            // Tạo đối tượng tổ chức nhóm từ DTO
            var organizationGroup = new OrganizationGroup
            {
                Code = postOrganizationGroupRequest.code,
                Name = postOrganizationGroupRequest.name,
                Description = postOrganizationGroupRequest.description,
                Status = postOrganizationGroupRequest.status
            };

            // Thêm tổ chức nhóm vào cơ sở dữ liệu thông qua repository
            await _unitOfWork.OrganizationGroupRepository.AddAsync(organizationGroup);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _unitOfWork.CommitAsync();
        }

        #region Delete Organization Group
        public async Task DeleteOrganizationGroupAsync(Guid id)
        {
            var existedOrganizationGroup = await _unitOfWork.OrganizationGroupRepository.GetOrganizationGroupByIdAsync(id);
            if (existedOrganizationGroup == null)
            {
                throw new Exception($"Organization Group with ID {id} cannot be found.");
            }

            await _unitOfWork.OrganizationGroupRepository.DeleteOrganizationGroupAsync(existedOrganizationGroup);
            await _unitOfWork.CommitAsync();
        }
        #endregion

        #region Get All Organization Groups
        public async Task<GetAllOrganizationGroupsResponse> GetAllOrganizationGroupsAsync(GetAllOrganizationGroupsRequest getAllOrganizationGroupsRequest)
        {
            var organizationGroups = await _unitOfWork.OrganizationGroupRepository.GetAllOrganizationGroupsAsync();
            if (organizationGroups == null || !organizationGroups.Any())
            {
                throw new Exception("No organization groups found.");
            }

            // Phân trang
            int totalRecords = organizationGroups.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / getAllOrganizationGroupsRequest.PageSize);

            var paginatedList = organizationGroups.Skip((getAllOrganizationGroupsRequest.PageNumber - 1) * getAllOrganizationGroupsRequest.PageSize)
                                                  .Take(getAllOrganizationGroupsRequest.PageSize)
                                                  .ToList();

            var responseList = _mapper.Map<List<GetOrganizationGroupResponse>>(paginatedList);

            return new GetAllOrganizationGroupsResponse
            {
                PageSize = getAllOrganizationGroupsRequest.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = getAllOrganizationGroupsRequest.PageNumber,
                OrganizationGroups = responseList
            };
        }
        #endregion

        #region Get Organization Group By ID
        public async Task<GetOrganizationGroupResponse> GetOrganizationGroupByIdAsync(Guid id)
        {
            var organizationGroup = await _unitOfWork.OrganizationGroupRepository.GetOrganizationGroupByIdAsync(id);
            if (organizationGroup == null)
            {
                throw new Exception($"Organization Group with ID {id} cannot be found.");
            }

            return _mapper.Map<GetOrganizationGroupResponse>(organizationGroup);
        }
        #endregion

        #region Update Organization Group
        public async Task UpdateOrganizationGroupAsync(Guid id, UpdateOrganizationGroupRequest updateOrganizationGroupRequest)
        {
            var existedOrganizationGroup = await _unitOfWork.OrganizationGroupRepository.GetOrganizationGroupByIdAsync(id);
            if (existedOrganizationGroup == null)
            {
                throw new Exception($"Organization Group with ID {id} cannot be found.");
            }

            // Cập nhật các thuộc tính của Organization Group từ request
            existedOrganizationGroup.Name = updateOrganizationGroupRequest.name;
            existedOrganizationGroup.Description = updateOrganizationGroupRequest.description;
            existedOrganizationGroup.Status = updateOrganizationGroupRequest.status;

            await _unitOfWork.OrganizationGroupRepository.UpdateOrganizationGroupAsync(existedOrganizationGroup);
            await _unitOfWork.CommitAsync();
        }
        #endregion
    }
}

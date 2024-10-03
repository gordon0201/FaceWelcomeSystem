using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
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

        public OrganizationGroupService(IUnitOfWork unitOfWork)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
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
    }
}

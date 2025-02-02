﻿
namespace UserUserManagment.AppService.Helper.Wrapper
{
    public class  PaginationFilter
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize,int totalRecords)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize!= 0 ? pageSize  : totalRecords;
        }
    }
}

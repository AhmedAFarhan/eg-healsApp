using EGHeals.Services.ApiRequests;
using EGHeals.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EGHeals.Services.Services
{
    public class EGService
    {
        private readonly RequestHandler _requestHandler;

        public EGService(RequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
        }

        public async Task<EGResponse<TOut>> GetAllAsync<TOut>(string url)
        {
            try
            {
                var result = await _requestHandler.GetRequest<EGResponse<TOut>>($"{url}");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EGResponse<TOut>> PostAsync<TOut, TIn>(string url, TIn dto, bool isFormData = false)
        {
            try
            {
                var result = isFormData ? await _requestHandler.PostFormDataRequest<EGResponse<TOut> , TIn>(url, dto) : await _requestHandler.PostRequest<EGResponse<TOut>, TIn>(url, dto);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EGResponse<TOut>> PostRangeAsync<TOut, TIn>(string url, List<TIn> dtos, bool isFormData = false)
        {
            try
            {
                var result = isFormData ? await _requestHandler.PostFormDataRequest<EGResponse<TOut>, List <TIn>>(url, dtos) : await _requestHandler.PostRequest<EGResponse<TOut>, List <TIn>>(url, dtos);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

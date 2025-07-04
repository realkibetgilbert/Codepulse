﻿using Codepulse.API.Application.DTOs.BlogPost;
using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Application.Features.Blog.Interfaces;
using Codepulse.API.Application.Mappers.Blog.Interfaces;
using Codepulse.API.Domain.Entities;
using Codepulse.API.Domain.Interfaces;
using Codepulse.API.Infrastructure.Catching;
using Microsoft.Extensions.Caching.Distributed;

namespace Codepulse.API.Application.Features.Blog.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IDistributedCache _cache;
        private readonly IBlogPostMapper _mapper;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostService(IDistributedCache cache, IBlogPostMapper mapper, IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _cache = cache;
            _mapper = mapper;
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BlogPostToDisplay> CreateAsync(CreateBlogPostRequestDto createBlogPostRequestDto)
        {
            var blogPostDomain = _mapper.MapToDomain(createBlogPostRequestDto);
            blogPostDomain.Categories = new List<Category>();   

            foreach (var categoryId in createBlogPostRequestDto.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                if (existingCategory != null)
                {
                    blogPostDomain.Categories.Add(existingCategory);
                }
            }

            blogPostDomain = await _blogPostRepository.CreateAsync(blogPostDomain);

            return _mapper.MapToDto(blogPostDomain);
        }
        public async Task<IEnumerable<BlogPostToDisplay>> GetAllAsync()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            return _mapper.MapToDto(blogPosts);
        }
        public async Task<BlogPostToDisplay?> GetByIdAsync(long id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            if (blogPost == null) return null;

            return _mapper.MapToDto(blogPost);
        }
        public async Task<BlogPostToDisplay?> GetByUrlHandleAsync(string urlHandle)
        {
            string cacheKey = $"blog:{urlHandle}";
            var cached = await _cache.GetOrSetAsync<BlogPostToDisplay>(
                cacheKey,
                async () =>
                {
                    var blogPost = await _blogPostRepository.GetByUrlAsync(urlHandle);
                    if (blogPost == null) return null;
                    return _mapper.MapToDto(blogPost);
                }
            );
            return cached;
        }
        public async Task<BlogPostToDisplay?> UpdateAsync(long id, BlogPostToUpdate blogPostToUpdate)
        {
            var blogPostDomain = _mapper.MapToDomain(blogPostToUpdate);
            blogPostDomain.Id = id;
            blogPostDomain.PublishedDate = DateTime.UtcNow;
            blogPostDomain.Categories = new List<Category>();
            foreach (var categoryId in blogPostToUpdate.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
                if (existingCategory != null)
                {
                    blogPostDomain.Categories.Add(existingCategory);
                }
            }

            var updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPostDomain);
            if (updatedBlogPost == null)
                return null;

            return _mapper.MapToDto(updatedBlogPost);
        }
        public async Task<BlogPostToDisplay?> DeleteAsync(long id)
        {
            var blogPost = await _blogPostRepository.DeleteAsync(id);
            if (blogPost == null) return null;

            return _mapper.MapToDto(blogPost);
        }

    }
}
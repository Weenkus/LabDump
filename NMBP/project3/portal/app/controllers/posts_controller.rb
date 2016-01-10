class PostsController < ApplicationController

	def home
	end
	
	def new
	end
	
	def create
		@post = Post.new(post_params)
		@post.save
	
		redirect_to action: "home"
	end
	
	
	private
		def post_params
		  params.require(:post).permit(:title, :image, :content, :author)
		end

end

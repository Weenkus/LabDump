class LatestPost
  include Mongoid::Document
  include Mongoid::Timestamps
  
  field :postId, type: String
end

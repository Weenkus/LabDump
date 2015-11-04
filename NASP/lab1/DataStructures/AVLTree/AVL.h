#pragma once

#include <vector>

namespace dataStructures {

	struct node {
		int element;
		node* left;
		node* right;
		node* parent;
		int height;

		node(int element) {
			this->element = element;
			this->left = nullptr;
			this->right = nullptr;
			this->height = 1;
		}

	};


	class AVL
	{
	public:
		AVL();
		AVL(std::vector<int> numbers);
		~AVL();

		void insert(int element);

		/*void insertAVL(int element);
		node*  AVL::insertAVL(node* treeNode, int element);*/

		void inorder() const;

		int treeHight() { return this->height(this->root); }

		void printPretty();

		void setRoot(node* root) { this->root = root;  }
		node* getRoot() const { return this->root; }


	private:
		node *root;

		void insert(int element, node** root, node* aboveNode);

		void inorder(node* root) const;

		int heightDifference(node* root);
		int height(node* root);

		void printBinaryTree(node *n);
		

		/*node* RRrotation(node* root);
		node* RLrotation(node* root);
		node* LLrotation(node* root);
		node* LRrotation(node* root);

		node* balance(node* temp);*/
	};

}


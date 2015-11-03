#include "AVL.h"

#include <stdio.h>
#include <algorithm>
#include <queue>
#include <utility>
#include <iostream>

namespace dataStructures {

	AVL::AVL()
	{
		this->root = nullptr;
	}

	AVL::AVL(std::vector<int> numbers) {
		this->root = nullptr;
		for (auto& num : numbers)
			this->insert(num);
	}


	AVL::~AVL()
	{
	}

	void AVL::insert(int element) {
		insert(element, &(this->root), nullptr);
	}

	void AVL::insert(int element, node** root, node* aboveNode) {
		if (*root == nullptr) {
			*root = new node(element);
			(*root)->parent = aboveNode;
			
		}
		else {
			if (element <= (*root)->element) {
				insert(element, &((*root)->left), *root);
				(*root)->height = this->height(*root);
			}
			else {
				insert(element, &((*root)->right), *root);
				(*root)->height = this->height(*root);
			}
		}
	}

	void AVL::inorder() const{
		inorder(this->root);
	}

	void AVL::inorder(node *root) const {
		if (root == nullptr)
			return;

		inorder(root->left);
		printf("%d ", root->element);
		inorder(root->right);
	}

	int AVL::height(node* root) {
		int height{ 0 };
		if (root != nullptr) {
			int maxHeight = std::max(AVL::height(root->left), AVL::height(root->right));
			height = maxHeight + 1;
		}
		return height;
	}

	int AVL::heightDifference(node * root)
	{
		return AVL::height(root->left) - AVL::height(root->right);
	}

	void AVL::printPretty() {
		printBinaryTree(this->root);
	}

	void AVL::printBinaryTree(node *n) {
		if (nullptr == n) {
			return;
		}
		int level = 0;

		// BFS
		typedef std::pair<node*, int> node_level;
		std::queue<node_level> q;
		q.push(node_level(n, 1));

		int height = this->treeHight();

		int counter{ 0 };
		while (!q.empty()) {
			node_level nl = q.front();
			q.pop();
			if (nullptr != (n = nl.first)) {
				if (level != nl.second) {
					std::cout << std::endl;
					std::cout << "Level " << nl.second << ": ";

					level = nl.second;
				}
				std::cout << n->element << " <" << n->height << ">    ";
				
				q.push(node_level(n->left, 1 + level));
				q.push(node_level(n->right, 1 + level));
			}
			++counter;
		}
		std::cout << std::endl;
	}

	/*node* AVL::RRrotation(node* root) {
		node* temp;
		temp = (root)->right;
		(root)->right = temp->left;
		temp->left = (root);
		return temp;
	}

	node* AVL::RLrotation(node* root) {
		node* temp;
		temp = (root)->left;
		(root)->left = temp->right;
		temp->right = (root);
		return temp;
	}

	node* AVL::LLrotation(node* root) {
		node* temp;
		temp = root->left;
		(root)->left = RRrotation(temp);
		return LLrotation(root);
	}

	node* AVL::LRrotation(node* root) {
		node* temp;
		temp = (root)->right;
		(root)->right = LLrotation(temp);
		return RRrotation(root);
	}

	node* AVL::balance(node* temp)
	{
		int bal_factor = heightDifference(temp);
		if (bal_factor > 1)
		{
			if (heightDifference(temp->left) > 0)
				temp = LLrotation(temp);
			else
				temp = LRrotation(temp);
		}
		else if (bal_factor < -1)
		{
			if (heightDifference(temp->right) > 0)
				temp = RLrotation(temp);
			else
				temp = RRrotation(temp);
		}
		return temp;
	}

	// A utility function to right rotate subtree rooted with y
	// See the diagram given above.
	node *rightRotate(struct node *y)
	{
		 node *x = y->left;
		 node *T2 = x->right;

		// Perform rotation
		x->right = y;
		y->left = T2;

		// Return new root
		return x;
	}

	// A utility function to left rotate subtree rooted with x
	// See the diagram given above.
	 node *leftRotate(struct node *x)
	{
		 node *y = x->right;
		 node *T2 = y->left;

		// Perform rotation
		y->left = x;
		x->right = T2;

		// Return new root
		return y;
	}*/
}
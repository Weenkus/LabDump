#include "AVL.h"

#include <stdio.h>
#include <algorithm>
#include <queue>
#include <utility>
#include <iostream>



namespace dataStructures {

	int heightDiff(node * root);
	node* rightRotate(node* y);
	node* leftRotate(node* x);

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
		/*int balance = heightDiff(*root);

		// If this node becomes unbalanced, then there are 4 cases

		// Left Left Case
		if (balance > 1 && element < (*root)->left->element) {
		*root = rightRotate(*root);
		std::cout << "LL" << std::endl;
		}

		// Right Right Case
		if (balance < -1 && element >(*root)->right->element) {
		*root = leftRotate(*root);
		std::cout << "RR" << std::endl;
		}

		// Left Right Case
		if (balance > 1 && element > (*root)->left->element)
		{
		std::cout << "LR" << std::endl;
		*root = leftRotate((*root)->left);
		*root = rightRotate(*root);
		}

		// Right Left Case
		if (balance < -1 && element < (*root)->right->element)
		{
		std::cout << "RL" << std::endl;
		*root = rightRotate(((*root)->right));
		*root = leftRotate(*root);
		}*/
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

	// Function used to calc the high for nodes inside the class
	int calcHeight(node *root) {
		int height{ 0 };
		if (root != nullptr) {
			int maxHeight = std::max(calcHeight(root->left), calcHeight(root->right));
			height = maxHeight + 1;
		}
		return height;
	}


	int AVL::heightDifference(node * root)
	{
		if (root == nullptr)
			return 0;
		return AVL::height(root->left) - AVL::height(root->right);
	}

	// HeightDiff for nodes inside the tree
	int heightDiff(node * root)
	{
		if (root == nullptr || root->left == nullptr || root->right == nullptr)
			return 0;
		return root->left->height - root->right->height;
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

	

	// A utility function to right rotate subtree rooted with y
	// See the diagram given above.
	 node *rightRotate( node *y)
	{
		 node *x = y->left;
		 node *T2 = x->right;

		// Perform rotation
		x->right = y;
		y->left = T2;

		// Update heights
		y->height = std::max(y->left->height, y->right->height) + 1;
		x->height = std::max(x->left->height, x->right->height) + 1;

		// Return new root
		return x;
	}

	// A utility function to left rotate subtree rooted with x
	// See the diagram given above.
	 node *leftRotate( node *x)
	{
		 node *y = x->right;
		 node *T2 = y->left;

		// Perform rotation
		y->left = x;
		x->right = T2;

		//  Update heights
		x->height = std::max(x->left->height, x->right->height) + 1;
		y->height = std::max(y->left->height, y->right->height) + 1;

		// Return new root
		return y;
	}

	 /*void AVL::insertAVL(int element) {
		 insertAVL(this->root, element);
	 }

	 node*  AVL::insertAVL(node* treeNode, int element)
	 {
	
		 if (treeNode == nullptr) {
			 node* newNode = new node(element);
			 return newNode;
		 }

		 if (element < treeNode->element)
			 treeNode->left = insertAVL(treeNode->left, element);
		 else
			 treeNode->right = insertAVL(treeNode->right, element);

	
		 treeNode->height = std::max(treeNode->left->height, treeNode->right->height) + 1;


	
		 int balance = heightDiff(treeNode);

		 // If this node becomes unbalanced, then there are 4 cases

		 // Left Left Case
		 if (balance > 1 && element < treeNode->left->element)
			 return rightRotate(treeNode);

		 // Right Right Case
		 if (balance < -1 && element > treeNode->right->element)
			 return leftRotate(treeNode);

		 // Left Right Case
		 if (balance > 1 && element > treeNode->left->element)
		 {
			 treeNode->left = leftRotate(treeNode->left);
			 return rightRotate(treeNode);
		 }

		 // Right Left Case
		 if (balance < -1 && element < treeNode->right->element)
		 {
			 treeNode->right = rightRotate(treeNode->right);
			 return leftRotate(treeNode);
		 }

	
		 return treeNode;
	 }*/

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
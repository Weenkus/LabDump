#include "RedBlackTree.h"

#include <iostream>

using namespace std;

RedBlackTree::RedBlackTree()
{
	this->root = nullptr;
}


RedBlackTree::~RedBlackTree()
{
}

void RedBlackTree::inorderTraversalPrint(node *root) const
{
	// Escape recursion
	if (root == nullptr)
		return;

	// Go left sub tree
	if (root->left != nullptr)
		inorderTraversalPrint(root->left);

	// Print node value
	if(root->color == color::BLACK )
		cout << "<" << root->number << "> ";
	else
		cout << root->number << " ";

	// Go right sub tree
	if (root->right != nullptr)
		inorderTraversalPrint(root->right);
}

void RedBlackTree::insert(node *&root, int element)
{
	// Escape recursion
	if (root == nullptr) {
		root = new node(element, color::RED);
		return;
	}

	if (element <= root->number)
		insert(root->left, element);
	else
		insert(root->right, element);

}

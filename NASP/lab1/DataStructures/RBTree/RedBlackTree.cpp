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

void RedBlackTree::inorderTraversalPrint() const {
	this->inorderTraversalPrint(this->root);
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

void RedBlackTree::insert(int element, node *leaf)
{
	if (element < leaf->number)
	{
		if (leaf->left != nullptr)
			insert(element, leaf->left);
		else
		{
			leaf->left = new node (element, color::RED);
		}
	}
	else if (element >= leaf->number)
	{
		if (leaf->right != nullptr)
			insert(element, leaf->right);
		else
		{
			leaf->left = new node(element, color::RED);
		}
	}
}

void RedBlackTree::insert(int element)
{
	if (root != nullptr)
		insert(element, root);
	else
	{
		root = new node(element, color::RED);
	}
}

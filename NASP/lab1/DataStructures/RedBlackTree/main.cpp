// RBTreeC.cpp : Defines the entry point for the console application.
//

#include <stdio.h>
#include <stdlib.h>

#include <vector>
#include <string>
#include <fstream>
#include <iostream>
#include <algorithm>
#include <queue>

enum COLOR {RED, BLACK};

using namespace std;

void exitPrgoram();
vector<int> parseInputFile(const string& fileName);

struct node {
	int key;
	struct node *left, *right, *parent;
	COLOR color;
};

typedef struct node *nodePointer;

struct node NIL;
nodePointer NILpointer = &NIL;


/*
	Use BFS to print the tree in rows.
*/
void printBinaryTree(nodePointer n) {
	if (NILpointer == n) {
		return;
	}
	int level = 0;

	// BFS
	typedef std::pair<node*, int> node_level;
	std::queue<node_level> q;
	q.push(node_level(n, 1));

	while (!q.empty()) {
		node_level nl = q.front();
		q.pop();
		if (NILpointer != (n = nl.first)) {
			if (level != nl.second) {
				std::cout << std::endl;
				std::cout << "Level " << nl.second << ": ";

				level = nl.second;
			}
			if(n->color == RED)
				std::cout << "<" << n->key << ">  ";
			else 
				std::cout << n->key << "  ";

			q.push(node_level(n->left, 1 + level));
			q.push(node_level(n->right, 1 + level));
		}
	}
	std::cout << std::endl;
}

/*
	Print the elements of the red black tree in inorder order, as the tree is a binary search
	tree, the inorder traversal will print elements from the lowest to the highest number (sorted).
	For the above to be true, left subtree must contain the values <= and the right subtree > of the 
	element in the current node we are watching.
*/
void inorder(nodePointer x) {
	if (x != NILpointer) {
		inorder(x->left);
		if (x->color == RED)
			printf(" <%d> ", x->key);
		else
			printf(" %d ", x->key);
		inorder(x->right);
	}
}

/*
	Search for the element in the BST, go left if you value is less than the current node, otherwise
	go right. If you node has you value voila, otherwise you will visit a NIL node meaning the tree
	doesn't contain the given element.
*/
nodePointer search(nodePointer root, int number) {
	if (root == NILpointer || root->key == number)
		return root;
	if (number < root->key)
		return search(root->left, number);
	else
		return search(root->right, number);
}

/*
	The minimum element of the tree can be found in the leftmost node.
*/
nodePointer minimum(nodePointer root) {
	while (root->left != NILpointer)
		root = root->left;
	return root;
}

/*
	The maximum element of the tree can be found in the rightmost node.
*/
nodePointer maximum(nodePointer root) {
	while (root->right != NILpointer)
		root = root->right;
	return root;
}

/*
	Left rotation of the tree nodes.
*/
void leftRotation(nodePointer *treeroot, nodePointer x) {
	nodePointer y = x->right;
	x->right = y->left;

	// Update parent pointer of y's left child
	if (y->left != NILpointer)
		y->left->parent = x;
	y->parent = x->parent;

	// If x's parent is null make y the ROOT of the tree
	if (x->parent == NILpointer)
		*treeroot = y;
	else if (x->parent->left == x)
		x->parent->left = y;
	else
		x->parent->right = y;
	y->left = x;
	x->parent = y;
}

/*
	Right rotation of the tree nodes.
*/
void rightRotation(nodePointer *treeroot, nodePointer y) {
	nodePointer x = y->left;
	y->left = x->right;

	// Update parent point of x's right child
	if (x->right != NILpointer)
		x->right->parent = y;
	x->parent = y->parent;

	// If y's parent is null make x the ROOT of the tree
	if (y->parent == NILpointer)
		*treeroot = x;
	else if (y->parent->left == y)
		y->parent->left = x;
	else
		y->parent->right = x;
	x->right = y;
	y->parent = x;
}

/*
	Fix the red black tree after an element is inserted into the tree.
*/
void insertFixUp(nodePointer *treeroot, nodePointer n) {
	// Iterate until n parent color is black
	while (n->parent->color == RED) {
		// LEFT DIAGONAL
		if (n->parent == n->parent->parent->left) {
			// Store uncle in u
			nodePointer u = n->parent->parent->right;

			// If uncle is RED, do following
			// (i)  Change color of parent and uncle as BLACK
			// (ii) Change color of grandparent as RED
			// (iii) Move n to grandparent
			if (u->color == RED) {
				n->parent->color = BLACK;
				u->color = BLACK;
				n->parent->parent->color = RED;
				n = n->parent->parent;
			}
			else {
				// N left from P
				// P right from G
				/*			 Rotation -> TURN "<" u "/"			*/
				if (n == n->parent->right) {
					n = n->parent;
					leftRotation(treeroot, n);
				}

				// N left from P
				// P left from G
				// Add color change to the rotation
				n->parent->color = BLACK;
				n->parent->parent->color = RED;
				rightRotation(treeroot, n->parent->parent);
			}
		}
		// RIGHT DIAGONAL
		else {
			// Store uncle in u
			nodePointer u = n->parent->parent->left;

			// If uncle is RED, do following
			// (i)  Change color of parent and uncle as BLACK
			// (ii) Change color of grandparent as RED
			// (iii) Move n to grandparent
			if (u->color == RED) {
				n->parent->color = BLACK;
				u->color = BLACK;
				n->parent->parent->color = RED;
				n = n->parent->parent;
			}
			else {
				// N right from P
				// P left from G
				// Rotation
				if (n == n->parent->left) {
					n = n->parent;
					/*		Rotation -> TURN ">" u "\"			*/
					rightRotation(treeroot, n);
				}

				// N right from P
				// P right from G
				// Add color change to the rotation
				n->parent->color = BLACK;
				n->parent->parent->color = RED;
				leftRotation(treeroot, n->parent->parent);
			}
		}
	}
	(*treeroot)->color = BLACK;
}

/*
	Insert the element into RB tree
*/
void insert(nodePointer *treeroot, int element) {
	nodePointer newNode = (nodePointer)malloc(sizeof(struct node));
	newNode->key = element;
	nodePointer y = NILpointer;
	nodePointer x = *treeroot;

	// Find the spot to insert the new node, also check if root exists
	while (x != NILpointer) {
		y = x;
		if (newNode->key < x->key)
			x = x->left;
		else
			x = x->right;
	}

	// Setup the new node
	newNode->parent = y;
	if (y == NILpointer)
		*treeroot = newNode;
	else if (newNode->key < y->key)
		y->left = newNode;
	else
		y->right = newNode;

	newNode->left = NILpointer;
	newNode->right = NILpointer;
	newNode->color = RED;

	// Fix the red black tree rules
	insertFixUp(treeroot, newNode);
}

void rbtransplant(nodePointer *treeroot, nodePointer u, nodePointer v) {
	if (u->parent == NILpointer)
		*treeroot = v;
	else if (u == u->parent->left)
		u->parent->left = v;
	else
		u->parent->right = v;
	v->parent = u->parent;
}

void rbdeletefixup(nodePointer *treeroot, nodePointer x) {
	while (x != *treeroot && x->color == BLACK) {
		if (x == x->parent->left) {
			nodePointer w = x->parent->right;
			if (w->color == RED) {
				w->color = BLACK;
				x->parent->color = RED;
				leftRotation(treeroot, x->parent);
				w = x->parent->right;
			}
			if (w->left->color == BLACK && w->right->color == BLACK) {
				w->color = RED;
				x = x->parent;
			}
			else {
				if (w->right->color == BLACK) {
					w->left->color = BLACK;
					w->color = RED;
					rightRotation(treeroot, w);
					w = x->parent->right;
				}
				w->color = x->parent->color;
				x->parent->color = BLACK;
				w->right->color = BLACK;
				leftRotation(treeroot, x->parent);
				x = *treeroot;
			}
		}
		else {
			nodePointer w = x->parent->left;
			if (w->color == RED) {
				w->color = BLACK;
				x->parent->color = RED;
				rightRotation(treeroot, x->parent);
				w = x->parent->left;
			}
			if (w->left->color == BLACK && w->right->color == BLACK) {
				w->color = RED;
				x = x->parent;
			}
			else {
				if (w->left->color == BLACK) {
					w->right->color = BLACK;
					w->color = RED;
					leftRotation(treeroot, w);
					w = x->parent->left;
				}
				w->color = x->parent->color;
				x->parent->color = BLACK;
				w->left->color = BLACK;
				rightRotation(treeroot, x->parent);
				x = *treeroot;
			}
		}
	}
	x->color = BLACK;
}

void rbdelete(nodePointer *treeroot, int z) {
	nodePointer Z = search(*treeroot, z);
	if (Z == NILpointer) {
		printf("Node to be deleted not found\n");
		return;
	}
	nodePointer y = Z;
	int yoc = y->color;
	nodePointer x;
	if (Z->left == NILpointer) {
		x = Z->right;
		rbtransplant(treeroot, Z, Z->right);
	}
	else if (Z->right == NILpointer) {
		x = Z->left;
		rbtransplant(treeroot, Z, Z->left);
	}
	else {
		y = minimum(Z->right);
		yoc = y->color;
		x = y->right;
		if (y->parent == Z)
			x->parent = y;
		else {
			rbtransplant(treeroot, y, y->right);
			y->right = Z->right;
			y->right->parent = y;
		}
		rbtransplant(treeroot, Z, y);
		y->left = Z->left;
		y->left->parent = y;
		y->color = Z->color;
	}
	if (yoc == BLACK)
		rbdeletefixup(treeroot, x);
}

int main(int argc, char* argv[])
{
	NIL.left = NIL.right = NIL.parent = NILpointer;
	NIL.color = BLACK;
	nodePointer tree = NILpointer;
	int n;

	// Parse the input file
	vector<int> numbers = parseInputFile(argv[1]);
	for (auto num : numbers) {
		insert(&tree, num);
	}
	cout << "\nStarting RedBlackTree interface...\n" << endl << "Example of a RED node \"<number>\", BLACK node \"number\"";

	while (1) {
		int n = 0;
		cout << "\n\n*** CHOOSE INPUT ***\n1.Insert\n2.Delete\n3.Print(inorder)\n4.Show levels\n5.Exit\n" << endl;
		cin >> n;
		int userInput = 0;
		if (n == 1) {
			cout << "Enter any number: ";
			cin >> userInput;
			insert(&tree, userInput);
		}
		else if (n == 2) {
			cout << "Enter the number to be deleted: ";
			cin >> userInput;
			rbdelete(&tree, userInput);
		}
		else if (n == 4) {
			printBinaryTree(tree);
		}
		else if (n == 5) {
			break;
		}
		if (n == 1 || n == 2 || n == 3) {
			inorder(tree);
		}
	}

	return 0;
}

vector<int> parseInputFile(const string& fileName) {
	ifstream inputFile;
	inputFile.open(fileName);

	// Read the file
	vector<int> parsedNumbers;
	const int expectedNumberOfInputElements{ 5 };
	parsedNumbers.reserve(expectedNumberOfInputElements);


	int counter{ 0 }, fileNumber{ 0 };
	while (inputFile >> fileNumber) {
		parsedNumbers.push_back(fileNumber);
	}

	// Print what was parsed
	cout << "Parsing complet, file contained:\n";
	for (auto numb : parsedNumbers)
		cout << numb << " ";
	cout << endl;

	return parsedNumbers;
}

void exitPrgoram() {
	cout << "\nEnter any input to exit the program.";
	getchar();
}

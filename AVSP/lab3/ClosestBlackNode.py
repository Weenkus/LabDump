import sys

MODE = 'LOCAL'
#MODE = 'SPRUT'

FILE_PATH = "lab3B_primjeri/btest2/R.in"


def main():
    number_of_nodes, number_of_edges, node_types, edges = parse_input(FILE_PATH)

    if MODE == 'LOCAL':
        print('Number of nodes: ', number_of_nodes)
        print('Number of edges: ', number_of_edges)
        print('Size of edge dictionary:', len(edges))
        #print(edges)
        #print(node_types)

    bfs_new(number_of_nodes, number_of_edges, node_types, edges)


def parse_input(file_path):
    if MODE == 'LOCAL':
        file_handle = open(file_path, "r")
        lines = file_handle.read().split("\n")
    else:
        lines = sys.stdin.readlines()

    node_types = []
    edges = {}
    for i, line in enumerate(lines):
        line_words = line.split(" ")

        if line == "":
            continue

        if i == 0:
            number_of_nodes = int(line_words[0])
            number_of_edges = int(line_words[1])
        elif 0 < i < 1 + number_of_nodes:
            node_types.append(int(line_words[0]))
        else:
            if int(line_words[0]) not in edges:
                edges[int(line_words[0])] = [int(line_words[1])]
            else:
                edges[int(line_words[0])].append(int(line_words[1]))

            if int(line_words[1]) not in edges:
                edges[int(line_words[1])] = [int(line_words[0])]
            else:
                edges[int(line_words[1])].append(int(line_words[0]))



    return number_of_nodes, number_of_edges, node_types, edges


def bfs_new(number_of_nodes, number_of_edges, node_types, edges):
    for node in range(0, number_of_nodes):

        if get_type(node, node_types) == 1:
            print(node, 0)
            continue

        queue = [[node]]
        visited = set()
        good_paths = []

        while queue:
            # Gets the first path in the queue
            path = queue.pop(0)

            # Gets the last node in the path
            vertex = path[-1]

            # Checks if we got to the end
            if get_type(vertex, node_types) == 1:
                good_paths.append(path)

            # We check if the current node is already in the visited nodes set in order not to recheck it
            elif vertex not in visited:
                # enumerate all adjacent nodes, construct a new path and push it into the queue
                for current_neighbour in [adjecant_node for adjecant_node in expand_node(vertex, edges)]:
                    new_path = list(path)
                    new_path.append(current_neighbour)
                    queue.append(new_path)

                # Mark the vertex as visited
                visited.add(vertex)

        if len(good_paths) == 0:
            print(-1, -1)
        else:
            best_paths = get_best_paths(good_paths)
            print_nearest_black_node(best_paths)


def print_nearest_black_node(best_paths):
    min_value = sys.maxsize
    for best_path in best_paths:
        if best_path[-1] < min_value:
            min_value = best_path[-1]

    print(min_value, len(best_paths[0])-1)


def get_best_paths(good_paths):
    min_value = min([len(e) for e in good_paths])
    return [e for e in good_paths if len(e) == min_value]

def expand_node(node, edges):
    return edges[node]


def get_type(node, node_types):
    return node_types[node]


def get_distance(path_map, start_node, end_node):
    path = [end_node]
    distance = 0
    while path[-1] != start_node:
        path.append(path_map[path[-1]])
        distance += 1
    path.reverse()
    #print(path)
    return len(path)-1


if __name__ == '__main__':
    main()

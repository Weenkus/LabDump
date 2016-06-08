import sys

#MODE = "SPRUT"
MODE = "LOCAL"

TIMESTAMP = 0

SIZE = 0
TS = 1


def main():
    N = 0
    buckets = []
    if MODE == "SPRUT":
        for index, line in enumerate(sys.stdin):
            if index == 0:
                N = int(line)
            else:
                buckets = stream(line, N, buckets)

    else:
        f = open('Primjeri/1.in', 'r')
        lines = f.read().split('\n')
        for index, line in enumerate(lines):
            if index == 0:
                N = int(line)
            else:
                buckets = stream(line, N, buckets)


# [0] SIZE, [1] TIMESTAMP
def stream(line, N, buckets):
    global TIMESTAMP

    if 'q' in line:
        query_value = int(line.split(" ")[1])

        k = 0
        number_of_ones = 0
        wrote_to_output = False
        if MODE == "LOCAL":
            print('q_v:', query_value, buckets)
        for i, bucket in enumerate(buckets):

            if i != 0:
                k += buckets[i-1][TS] - buckets[i][TS]
                #print('k:', k)

            if k >= query_value:
                number_of_ones -= buckets[i-1][SIZE]/2
                print(int(number_of_ones), k)
                wrote_to_output = True
                break
            else:
                number_of_ones += bucket[SIZE]

        if not wrote_to_output:
            output = int(number_of_ones) - int(buckets[-1][SIZE]/2)
            print("K:", output)
    else:
        for c in line:

            if len(buckets) > 2 and (buckets[0][TS] - buckets[-1][TS]) > N:
                buckets.pop()

            if c == '1':
                buckets.insert(0, [1, TIMESTAMP])

                for i, bucket in enumerate(buckets):

                    if i > 1 and buckets[i][SIZE] == buckets[i-1][SIZE] == buckets[i-2][SIZE]:
                        buckets.remove(buckets[i])
                        buckets[i-1][SIZE] *= 2

            TIMESTAMP += 1

    return buckets

if __name__ == '__main__':
    main()

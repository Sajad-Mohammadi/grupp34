

export default function trimString(string, length) {
    if (string.length <= length) {
        return string;
    }
    return string.substring(0, length) + '...';
}